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
    private userService: UserService
  ) {}

  async ngOnInit() {
    const roomIds = this.user?.rooms?.map((room) => room.id);
    this.reservationService.specParams.set({
      pageSize: 10,
      pageIndex: 1,
      roomId: roomIds?.join(','),
    });
    await this.init();
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

  onRefuse(id: string, status: string) {
    if (status === 'Pending') {
      this.dialogService
        .form(ReservationRefuseComponent, id, 'md')
        .then(async () => {
          this.toastService.success('Đã từ chối đặt lịch');
          await this.init();
        });
    } else {
      this.toastService.warn('Bạn không thể từ chối đặt lịch lúc này!');
    }
  }

  onAccept(id: string, status: string) {
    if (status === 'Pending') {
      this.dialogService
        .confirm({
          title: 'Xác nhận đặt lịch',
          content: 'Bạn có chắc muốn chấp nhận yêu cầu này không?',
          button: {
            accept: 'Chấp nhận',
            decline: 'Hủy bỏ',
          },
        })
        .then(async () => {
          const response = await firstValueFrom(
            this.reservationService.response(id, 'Confirmed').pipe(
              takeUntilDestroyed(this.destroyRef),
              catchError(() => of(null))
            )
          );
          if (response?.success) {
            this.toastService.success('Đã chấp nhận đặt lịch');
            await this.init();
          }
        });
    } else {
      this.toastService.warn('Bạn không thể chấp nhận đặt lịch lúc này!');
    }
  }
}
