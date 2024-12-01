import { SearchComponent } from '@shared/components/form/search/search.component';
import { PaginationComponent } from '@shared/components/pagination/pagination.component';
import { DialogService } from '@shared/services/dialog/dialog.service';
import { PaginationParams } from '@shared/models/pagination-params.model';
import { MiniLoadComponent } from '@shared/components/mini-load/mini-load.component';
import { catchError, lastValueFrom, of } from 'rxjs';
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
import { StatusInvoice } from '@features/booking/models/invoice.model';
import { LessorBookingDetailComponent } from '../../components/lessor-booking-detail/lessor-booking-detail.component';

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
    'tenant',
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

  @ViewChild(MatSort) sort?: MatSort;

  constructor(
    private dialogService: DialogService,
    private bookingService: BookingService,
    private userService: UserService
  ) {}

  async ngOnInit() {
    const roomIds = this.user?.rooms?.map((room) => room.id);
    if (roomIds?.length) {
      this.bookingService.specParams.set({
        pageSize: 10,
        pageIndex: 1,
        roomId: roomIds?.join(','),
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

  async onSearch(query: string) {
    this.bookingService.specParams.update((value) => ({
      ...value,
      search: query,
      pageIndex: 1,
    }));
    await this.init();
  }

  onLease(id: string, status: string) {
    if (status === 'Confirmed') {
    this.dialogService.view(LessorBookingDetailComponent, booking, 'xl');
    }
  }

  onDetail(booking: Booking) {
    this.dialogService.view(LessorBookingDetailComponent, booking, 'xl');
  }
}
