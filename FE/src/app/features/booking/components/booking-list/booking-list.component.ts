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
import { catchError, firstValueFrom, lastValueFrom, of } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { UserService } from '@core/services/user.service';
import { Booking, StatusBooking } from '@features/booking/models/booking.model';
import { BookingService } from '@features/booking/services/booking.service';
import { BookingViewComponent } from '../booking-view/booking-view.component';
import { BookingUpdateComponent } from '../booking-update/booking-update.component';

@Component({
  selector: 'app-booking-list',
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
  templateUrl: './booking-list.component.html',
  styleUrl: './booking-list.component.scss',
})
export class BookingListComponent implements OnInit {
  destroyRef = inject(DestroyRef);
  user = this.userService.currentUser();
  displayedColumns: string[] = [
    'name',
    'numberOfTenant',
    'intendedIntoDate',
    'endDate',
    'price',
    'deposite',
    'statusLease',
    'status',
    'optional',
  ];
  dataSource = new MatTableDataSource<Booking>();
  paginationParams = signal<PaginationParams>({
    pageSize: 0,
    count: 0,
    pageIndex: 1,
  });
  StatusBookingFilter = StatusBooking;

  @ViewChild(MatSort) sort?: MatSort;

  constructor(
    private dialogService: DialogService,
    private toastService: ToastService,
    private bookingService: BookingService,
    private userService: UserService
  ) {}

  async ngOnInit() {
    this.bookingService.specParams.set({
      pageSize: 10,
      pageIndex: 1,
      tenantId: this.user?.id,
    });
    await this.init();
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

  async onSearch(query: string) {
    this.bookingService.specParams.update((value) => ({
      ...value,
      search: query,
      pageIndex: 1,
    }));
    await this.init();
  }

  onView(booking: Booking) {
    this.dialogService.view(BookingViewComponent, booking);
  }

  onUpdate(booking: Booking) {
    if (booking.status !== 'Confirmed' && booking.status !== 'Rejected') {
      this.dialogService
        .form(BookingUpdateComponent, booking, 'lg')
        .then(async () => {
          this.toastService.success('Cập nhật đặt phòng thành công!');
          await this.init();
        });
    } else {
      this.toastService.warn('Bạn không thể sửa đặt phòng lúc này!');
    }
  }

  onDelete(id: string, status: string) {
    if (status !== 'Confirmed') {
      this.dialogService
        .confirm({
          title: 'Xác nhận xóa đặt phòng này không',
          content: 'Bạn có chắc muốn xóa đặt phòng này không?',
          button: {
            accept: 'Xóa',
            decline: 'Hủy bỏ',
          },
        })
        .then(async () => {
          const response = await firstValueFrom(
            this.bookingService.delete(id).pipe(
              takeUntilDestroyed(this.destroyRef),
              catchError(() => of(null))
            )
          );
          if (response?.success) {
            this.toastService.success('Xóa đặt phòng thành công');
            await this.init();
          }
        });
    } else {
      this.toastService.warn('Bạn không thể hủy đặt phòng lúc này!');
    }
  }
}
