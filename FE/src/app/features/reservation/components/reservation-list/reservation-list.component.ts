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
import { SearchComponent } from '@shared/components/form/search/search.component';
import { PaginationComponent } from '@shared/components/pagination/pagination.component';
import { DialogService } from '@shared/services/dialog/dialog.service';
import { PaginationParams } from '@shared/models/pagination-params.model';
import { MiniLoadComponent } from '@shared/components/mini-load/mini-load.component';
import { ToastService } from '@shared/services/toast/toast.service';
import { catchError, lastValueFrom, of } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import {
  Reservation,
  StatusReservation,
} from '@features/reservation/models/reservation.model';
import { ReservationService } from '@features/reservation/services/reservation.service';
import { UserService } from '@core/services/user.service';
import { ReservationUpdateComponent } from '../reservation-update/reservation-update.component';
import { ReservationViewComponent } from '../reservation-view/reservation-view.component';

@Component({
  selector: 'app-reservation-list',
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
  templateUrl: './reservation-list.component.html',
  styleUrl: './reservation-list.component.scss',
})
export class ReservationListComponent implements OnInit {
  destroyRef = inject(DestroyRef);
  user = this.userService.currentUser();
  displayedColumns: string[] = [
    'roomName',
    'createdAt',
    'reservationDate',
    'rejectionReason',
    'status',
    'optional',
  ];
  dataSource = new MatTableDataSource<Reservation>();
  paginationParams = signal<PaginationParams>({
    pageSize: 0,
    count: 0,
    pageIndex: 1,
  });
  statusReservationFilter = StatusReservation;

  @ViewChild(MatSort) sort?: MatSort;

  constructor(
    private dialogService: DialogService,
    private toastService: ToastService,
    private reservationService: ReservationService,
    private userService: UserService
  ) {}

  async ngOnInit() {
    if (this.user) {
      this.reservationService.specParams.set({
        pageSize: 10,
        pageIndex: 1,
        tenantId: this.user.id,
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

  onView(reservation: Reservation) {
    this.dialogService.view(ReservationViewComponent, reservation);
  }

  onUpdate(reservation: Reservation) {
    if (reservation.status === 'Pending') {
      this.dialogService
        .form(ReservationUpdateComponent, reservation, 'lg')
        .then(async () => {
          this.toastService.success('Cập nhật đặt lịch thành công!');
          await this.init();
        });
    } else {
      this.toastService.warn('Bạn không thể sửa đặt lịch lúc này!');
    }
  }

  onCancel(id: string, status: string) {
    if (status === 'Pending') {
      this.dialogService
        .confirm({
          title: 'Xác nhận hủy đặt lịch',
          content: 'Bạn có chắc muốn hủy đặt lịch này không?',
          button: {
            accept: 'Hủy hẹn',
            decline: 'Hủy bỏ',
          },
        })
        .then(() => {
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
                this.toastService.success('Hủy đặt lịch thành công');
              }
            });
        });
    } else {
      this.toastService.warn('Bạn không thể hủy đặt lịch lúc này!');
    }
  }
}
