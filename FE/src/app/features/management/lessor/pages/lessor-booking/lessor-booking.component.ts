import { ConditionRoom } from '@features/apartment/models/room.model';
import { SearchComponent } from '@shared/components/form/search/search.component';
import { PaginationComponent } from '@shared/components/pagination/pagination.component';
import { DialogService } from '@shared/services/dialog/dialog.service';
import { PaginationParams } from '@shared/models/pagination-params.model';
import { MiniLoadComponent } from '@shared/components/mini-load/mini-load.component';
import { ToastService } from '@shared/services/toast/toast.service';
import { catchError, firstValueFrom, lastValueFrom, of } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { UserService } from '@core/services/user.service';
import {
  Component,
  DestroyRef,
  inject,
  OnInit,
  signal,
  ViewChild,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { MatSortModule, MatSort } from '@angular/material/sort';
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { Booking, StatusBooking } from '@features/booking/models/booking.model';
import { BookingService } from '@features/booking/services/booking.service';
import { BookingRefuseComponent } from '@features/booking/components/booking-refuse/booking-refuse.component';

@Component({
  selector: 'app-lessor-booking',
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
  templateUrl: './lessor-booking.component.html',
  styleUrl: './lessor-booking.component.scss',
})
export class LessorBookingComponent implements OnInit {
  destroyRef = inject(DestroyRef);
  user = this.userService.currentUser();
  displayedColumns: string[] = [
    'roomName',
    'tenantName',
    'tenantNumberPhone',
    'numberOfTenant',
    'intendedIntoDate',
    'endDate',
    'roomStatus',
    'paymentStatus',
    'leaseStatus',
    'status',
    'optional',
  ];
  dataSource = new MatTableDataSource<Booking>();
  paginationParams = signal<PaginationParams>({
    pageSize: 0,
    count: 0,
    pageIndex: 1,
  });
  conditionFilter = ConditionRoom;
  StatusBookingFilter = StatusBooking;

  @ViewChild(MatSort) sort?: MatSort;

  constructor(
    private dialogService: DialogService,
    private toastService: ToastService,
    private bookingService: BookingService,
    private userService: UserService
  ) {}

  async ngOnInit() {
    const roomIds = this.user?.rooms?.map((room) => room.id);
    this.bookingService.specParams.set({
      pageSize: 10,
      pageIndex: 1,
      roomId: roomIds?.join(','),
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

  onRefuse(id: string, status: string) {
    if (status === 'Pending') {
      this.dialogService
        .form(BookingRefuseComponent, id, 'md')
        .then(async () => {
          this.toastService.success('Đã từ chối đặt phòng!');
          await this.init();
        });
    } else {
      this.toastService.warn('Bạn không thể từ chối đặt phòng lúc này!');
    }
  }

  onAccept(id: string, status: string) {
    if (status === 'Pending') {
      this.dialogService
        .confirm({
          title: 'Xác nhận đặt phòng',
          content: 'Bạn có chắc muốn chấp nhận yêu cầu này không?',
          button: {
            accept: 'Chấp nhận',
            decline: 'Hủy bỏ',
          },
        })
        .then(async () => {
          const response = await firstValueFrom(
            this.bookingService.accept(id).pipe(
              takeUntilDestroyed(this.destroyRef),
              catchError(() => of(null))
            )
          );
          if (response?.success) {
            this.toastService.success('Đã chấp nhận đặt phòng');
            await this.init();
          }
        });
    } else {
      this.toastService.warn('Bạn không thể chấp nhận đặt phòng lúc này!');
    }
  }
}