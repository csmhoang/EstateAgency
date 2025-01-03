import { CommonModule } from '@angular/common';
import {
  Component,
  DestroyRef,
  inject,
  OnInit,
  signal,
  ViewChild,
} from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { ConditionRoom } from '@features/apartment/models/room.model';
import { SearchComponent } from '@shared/components/form/search/search.component';
import { PaginationComponent } from '@shared/components/pagination/pagination.component';
import { DialogService } from '@shared/services/dialog/dialog.service';
import { PaginationParams } from '@shared/models/pagination-params.model';
import { MiniLoadComponent } from '@shared/components/mini-load/mini-load.component';
import { ToastService } from '@shared/services/toast/toast.service';
import { catchError, firstValueFrom, lastValueFrom, of } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import {
  Reservation,
  StatusReservation,
} from '@features/reservation/models/reservation.model';
import { ReservationService } from '@features/reservation/services/reservation.service';
import { UserService } from '@core/services/user.service';
import { ReservationRefuseComponent } from '@features/reservation/components/reservation-refuse/reservation-refuse.component';
import { PresenceService } from '@core/services/presence.service';
import { Notice } from '@features/notification/models/notification.model';

@Component({
  selector: 'app-lessor-reservation',
  standalone: true,
  imports: [
    MatTableModule,
    MatSortModule,
    MatButtonModule,
    MatMenuModule,
    PaginationComponent,
    SearchComponent,
    CommonModule,
    MiniLoadComponent,
  ],
  templateUrl: './lessor-reservation.component.html',
  styleUrl: './lessor-reservation.component.scss',
})
export class LessorReservationComponent implements OnInit {
  destroyRef = inject(DestroyRef);
  user = this.userService.currentUser();
  displayedColumns: string[] = [
    'roomName',
    'tenantName',
    'tenantNumberPhone',
    'reservationDate',
    'note',
    'roomStatus',
    'status',
    'optional',
  ];
  dataSource = new MatTableDataSource<Reservation>();
  paginationParams = signal<PaginationParams>({
    pageSize: 0,
    count: 0,
    pageIndex: 1,
  });
  conditionFilter = ConditionRoom;
  statusReservationFilter = StatusReservation;

  @ViewChild(MatSort) sort?: MatSort;

  constructor(
    private dialogService: DialogService,
    private toastService: ToastService,
    private reservationService: ReservationService,
    private userService: UserService,
    private presenceService: PresenceService
  ) {}

  async ngOnInit() {
    const roomIds = this.user?.rooms?.map((room) => room.id);
    if (roomIds?.length) {
      this.reservationService.specParams.set({
        pageSize: 10,
        pageIndex: 1,
        roomId: roomIds?.join(','),
      });
      await this.init();
    }
  }

  async init() {
    await lastValueFrom(
      this.reservationService.loadData().pipe(
        takeUntilDestroyed(this.destroyRef),
        catchError(() => of(null))
      )
    );

    const page = this.reservationService.page();
    if (page) {
      this.paginationParams.set(page);
      this.dataSource.data = page.data;
      if (this.sort) {
        this.dataSource.sort = this.sort;
      }
    }
  }

  async onPageChange(pageIndex: number) {
    this.reservationService.specParams.update((value) => ({
      ...value,
      pageIndex: pageIndex,
    }));
    await this.init();
  }

  async onSearch(query: string) {
    this.reservationService.specParams.update((value) => ({
      ...value,
      search: query,
      pageIndex: 1,
    }));
    await this.init();
  }

  onRefuse(id: string, status: string, tenantId: string, roomName: string) {
    if (status === 'Pending') {
      this.dialogService
        .form(ReservationRefuseComponent, id, 'md')
        .then(async () => {
          if (this.dataSource.data) {
            for (let i = 0; i < this.dataSource.data.length; i++) {
              if (this.dataSource.data[i].id === id) {
                this.dataSource.data[i].status = 'Rejected';
                this.dataSource._updateChangeSubscription();
                break;
              }
            }

            await this.presenceService.createNotification({
              receiverId: tenantId,
              title: 'Hẹn lịch xem phòng',
              content: `${this.user?.fullName} đã từ chối yêu cầu hẹn lịch xem phòng ${roomName}.`,
            } as Notice);

            this.toastService.success('Đã từ chối đặt lịch');
          }
        });
    } else {
      this.toastService.warn('Bạn không thể từ chối đặt lịch lúc này!');
    }
  }

  onAccept(
    id: string,
    status: string,
    condition: string,
    reservationDate: Date,
    tenantId: string,
    roomName: string,
    visibility: boolean
  ) {
    if (status === 'Pending') {
      if (condition === 'Occupied') {
        return this.toastService.warn('Phòng không có sẵn.');
      }
      if (!visibility) {
        return this.toastService.warn('Phòng đã bị xóa.');
      }

      if (new Date(reservationDate) >= new Date()) {
        this.dialogService
          .confirm({
            title: 'Xác nhận đặt lịch',
            content: 'Bạn có chắc muốn chấp nhận yêu cầu này không?',
            button: {
              accept: 'Chấp nhận',
              decline: 'Hủy bỏ',
            },
          })
          .then(() => {
            this.reservationService
              .response(id, 'Confirmed')
              .pipe(
                takeUntilDestroyed(this.destroyRef),
                catchError(() => of(null))
              )
              .subscribe(async (response) => {
                if (response?.success) {
                  for (let i = 0; i < this.dataSource.data.length; i++) {
                    if (this.dataSource.data[i].id === id) {
                      this.dataSource.data[i].status = 'Confirmed';
                      this.dataSource._updateChangeSubscription();
                      break;
                    }
                  }
                  await this.presenceService.createNotification({
                    receiverId: tenantId,
                    title: 'Hẹn lịch xem phòng',
                    content: `${this.user?.fullName} đã chấp nhận yêu cầu hẹn lịch xem phòng ${roomName}.`,
                  } as Notice);
                  this.toastService.success('Đã chấp nhận đặt lịch');
                }
              });
          });
      } else {
        this.reservationService
          .response(id, 'Canceled')
          .pipe(
            takeUntilDestroyed(this.destroyRef),
            catchError(() => of(null))
          )
          .subscribe((response) => {
            if (response?.success) {
              for (let i = 0; i < this.dataSource.data.length; i++) {
                if (this.dataSource.data[i].id === id) {
                  this.dataSource.data[i].status = 'Canceled';
                  this.dataSource._updateChangeSubscription();
                  break;
                }
              }
              this.toastService.warn('Đặt lịch này ngày yêu cầu đã quá hạn!');
            }
          });
      }
    } else {
      this.toastService.warn('Bạn không thể chấp nhận đặt lịch lúc này!');
    }
  }
}
