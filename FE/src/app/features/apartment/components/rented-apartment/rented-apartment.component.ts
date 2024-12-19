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
import { RouterLink } from '@angular/router';
import {
  Category,
  ConditionRoom,
  Room,
} from '@features/apartment/models/room.model';
import { SearchComponent } from '@shared/components/form/search/search.component';
import { PaginationComponent } from '@shared/components/pagination/pagination.component';
import { DialogService } from '@shared/services/dialog/dialog.service';
import { PaginationParams } from '@shared/models/pagination-params.model';
import { MiniLoadComponent } from '@shared/components/mini-load/mini-load.component';
import { ApartmentViewComponent } from '@features/apartment/components/apartment-view/apartment-view.component';
import { catchError, lastValueFrom, of } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { UserService } from '@core/services/user.service';
import { BookingDetailService } from '@features/booking/services/booking-detail.service';
import { BookingDetail } from '@features/booking/models/booking-detail.model';
import { Booking } from '@features/booking/models/booking.model';
import { LeaseViewComponent } from '@features/booking/components/lease-view/lease-view.component';
import { MessengerComponent } from '@features/messenger/components/messenger/messenger.component';

@Component({
  selector: 'app-rented-apartment',
  standalone: true,
  imports: [
    MatTableModule,
    MatSortModule,
    MatButtonModule,
    MatMenuModule,
    PaginationComponent,
    SearchComponent,
    RouterLink,
    CommonModule,
    MiniLoadComponent,
  ],
  templateUrl: './rented-apartment.component.html',
  styleUrl: './rented-apartment.component.scss',
})
export class RentedApartmentComponent implements OnInit {
  destroyRef = inject(DestroyRef);
  user = this.userService.currentUser();
  displayedColumns: string[] = [
    'roomName',
    'landlordName',
    'startDate',
    'endDate',
    'numberOfTenant',
    'price',
    'roomDetail',
    'optional',
  ];
  dataSource = new MatTableDataSource<BookingDetail>();
  paginationParams = signal<PaginationParams>({
    pageSize: 0,
    count: 0,
    pageIndex: 1,
  });
  conditionFilter = ConditionRoom;
  categoryFilter = Category;

  @ViewChild(MatSort) sort?: MatSort;

  constructor(
    private dialogService: DialogService,
    private bookingDetailService: BookingDetailService,
    private userService: UserService
  ) {}

  async ngOnInit() {
    if (this.user) {
      this.bookingDetailService.specParams.set({
        pageSize: 10,
        pageIndex: 1,
      });
      await this.init();
    }
  }

  async init() {
    await lastValueFrom(
      this.bookingDetailService.loadData().pipe(
        takeUntilDestroyed(this.destroyRef),
        catchError(() => of(null))
      )
    );

    const page = this.bookingDetailService.page();
    if (page) {
      this.paginationParams.set(page);
      this.dataSource.data = page.data;
      if (this.sort) {
        this.dataSource.sort = this.sort;
      }
    }
  }

  async onPageChange(pageIndex: number) {
    this.bookingDetailService.specParams.update((value) => ({
      ...value,
      pageIndex: pageIndex,
    }));
    await this.init();
  }

  async onSearch(query: string) {
    this.bookingDetailService.specParams.update((value) => ({
      ...value,
      search: query,
      pageIndex: 1,
    }));
    await this.init();
  }

  onView(room: Room) {
    this.dialogService.view(ApartmentViewComponent, room);
  }

  onLease(booking: Booking) {
    this.dialogService.view(LeaseViewComponent, booking, 'lg');
  }

  onChat(otherId?: string) {
    this.dialogService.form(MessengerComponent, otherId);
  }
}
