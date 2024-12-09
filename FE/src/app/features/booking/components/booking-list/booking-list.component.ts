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
import { PaginationComponent } from '@shared/components/pagination/pagination.component';
import { DialogService } from '@shared/services/dialog/dialog.service';
import { PaginationParams } from '@shared/models/pagination-params.model';
import { MiniLoadComponent } from '@shared/components/mini-load/mini-load.component';
import { ToastService } from '@shared/services/toast/toast.service';
import { catchError, lastValueFrom, of } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { UserService } from '@core/services/user.service';
import { Booking, StatusBooking } from '@features/booking/models/booking.model';
import { BookingService } from '@features/booking/services/booking.service';
import { BookingDetailComponent } from '../booking-detail/booking-detail.component';
import { StatusInvoice } from '@features/booking/models/invoice.model';
import { StatusLease } from '@features/booking/models/lease.model';
import { LeaseViewComponent } from '../lease-view/lease-view.component';

@Component({
  selector: 'app-booking-list',
  standalone: true,
  imports: [
    MatTableModule,
    MatSortModule,
    MatButtonModule,
    MatMenuModule,
    PaginationComponent,
    CommonModule,
    MiniLoadComponent,
  ],
  templateUrl: './booking-list.component.html',
  styleUrl: './booking-list.component.scss',
})
export class BookingListComponent implements OnInit {
  destroyRef = inject(DestroyRef);
  user = this.userService.currentUser();
  displayedColumns: string[] = [
    'landlord',
    'createdAt',
    'paymentStatus',
    'leaseStatus',
    'status',
    'detail',
    'optional',
  ];
  dataSource = new MatTableDataSource<Booking>();
  paginationParams = signal<PaginationParams>({
    pageSize: 0,
    count: 0,
    pageIndex: 1,
  });
  statusBookingFilter = StatusBooking;
  statusInvoiceFilter = StatusInvoice;
  statusLeaseFilter = StatusLease;

  @ViewChild(MatSort) sort?: MatSort;

  constructor(
    private dialogService: DialogService,
    private toastService: ToastService,
    private bookingService: BookingService,
    private userService: UserService
  ) {}

  async ngOnInit() {
    if (this.user) {
      this.bookingService.specParams.set({
        pageSize: 10,
        pageIndex: 1,
        tenantId: this.user.id,
      });
      await this.init();
    }
  }

  async init() {
    await lastValueFrom(
      this.bookingService.loadData().pipe(
        takeUntilDestroyed(this.destroyRef),
        catchError(() => of(null))
      )
    );

    const page = this.bookingService.page();
    if (page) {
      this.paginationParams.set(page);
      this.dataSource.data = page.data;
      if (this.sort) {
        this.dataSource.sort = this.sort;
      }
    }
  }

  async onPageChange(pageIndex: number) {
    this.bookingService.specParams.update((value) => ({
      ...value,
      pageIndex: pageIndex,
    }));
    await this.init();
  }

  onDetail(booking: Booking) {
    this.dialogService.view(BookingDetailComponent, booking);
  }

  onCancel(id: string, status: string) {
    if (status === 'Pending') {
      this.dialogService
        .confirm({
          title: 'Xác nhận hủy đặt phòng này không',
          content: 'Bạn có chắc muốn hủy đặt phòng này không?',
          button: {
            accept: 'Hủy đặt',
            decline: 'Hủy bỏ',
          },
        })
        .then(() => {
          this.bookingService
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
                this.toastService.success('Hủy đặt phòng thành công');
              }
            });
        });
    } else {
      this.toastService.warn('Bạn không thể hủy đặt phòng lúc này!');
    }
  }

  onLease(booking: Booking) {
    this.dialogService.view(LeaseViewComponent, booking, 'lg');
  }
}
