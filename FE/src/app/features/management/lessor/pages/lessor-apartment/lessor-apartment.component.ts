import { CommonModule, CurrencyPipe } from '@angular/common';
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
import { LessorApartmentService } from '../../services/lessor-apartment.service';
import { PaginationParams } from '@shared/models/pagination-params.model';
import { MiniLoadComponent } from '@shared/components/mini-load/mini-load.component';
import { ApartmentViewComponent } from '@features/apartment/components/apartment-view/apartment-view.component';
import { ApartmentUpdateComponent } from '@features/apartment/components/apartment-update/apartment-update.component';
import { ToastService } from '@shared/services/toast/toast.service';
import { catchError, firstValueFrom, lastValueFrom, of } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { UserService } from '@core/services/user.service';

@Component({
  selector: 'app-lessor-apartment',
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
  templateUrl: './lessor-apartment.component.html',
  styleUrl: './lessor-apartment.component.scss',
})
export class LessorApartmentComponent implements OnInit {
  destroyRef = inject(DestroyRef);
  user = this.userService.currentUser();
  displayedColumns: string[] = [
    'name',
    'category',
    'address',
    'area',
    'price',
    'createdAt',
    'condition',
    'optional',
  ];
  dataSource = new MatTableDataSource<Room>();
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
    private toastService: ToastService,
    private lessorApartmentService: LessorApartmentService,
    private userService: UserService
  ) {}

  async ngOnInit() {
    if (this.user) {
      this.lessorApartmentService.specParams.set({
        pageSize: 10,
        pageIndex: 1,
        landlordId: this.user.id,
      });
      await this.init();
    }
  }

  async init() {
    await lastValueFrom(
      this.lessorApartmentService.loadData().pipe(
        takeUntilDestroyed(this.destroyRef),
        catchError(() => of(null))
      )
    );

    const page = this.lessorApartmentService.page();
    if (page) {
      this.paginationParams.set(page);
      this.dataSource.data = page.data;
      if (this.sort) {
        this.dataSource.sort = this.sort;
      }
    }
  }

  async onPageChange(pageIndex: number) {
    this.lessorApartmentService.specParams.update((value) => ({
      ...value,
      pageIndex: pageIndex,
    }));
    await this.init();
  }

  async onSearch(query: string) {
    this.lessorApartmentService.specParams.update((value) => ({
      ...value,
      search: query,
      pageIndex: 1,
    }));
    await this.init();
  }

  onView(room: Room) {
    this.dialogService.view(ApartmentViewComponent, room);
  }

  onUpdate(room: Room) {
    this.dialogService.form(ApartmentUpdateComponent, room).then(async () => {
      this.toastService.success('Cập nhật phòng thành công!');
      await this.init();
    });
  }

  onHide(roomId: string) {
    this.dialogService
      .confirm({
        title: 'Xác nhận ẩn phòng trọ',
        content: 'Bạn có chắc muốn ẩn phòng trọ này không?',
        button: {
          accept: 'Ẩn',
          decline: 'Hủy bỏ',
        },
      })
      .then(async () => {
        const response = await firstValueFrom(
          this.lessorApartmentService.hide(roomId).pipe(
            takeUntilDestroyed(this.destroyRef),
            catchError(() => of(null))
          )
        );
        if (response?.success) {
          this.dataSource.data = this.dataSource.data.filter(
            (room) => room.id !== roomId
          );
          if (this.sort) {
            this.dataSource.sort = this.sort;
          }
          this.dataSource._updateChangeSubscription();
          this.toastService.success('Ẩn phòng trọ thành công!');
        }
      });
  }
}
