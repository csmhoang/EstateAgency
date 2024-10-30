import { CommonModule } from '@angular/common';
import {
  Component,
  inject,
  OnInit,
  signal,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { RouterLink } from '@angular/router';
import {
  Category,
  Condition,
  Room,
} from '@features/apartment/models/room.model';
import { SearchComponent } from '@shared/components/form/search/search.component';
import { PaginationComponent } from '@shared/components/pagination/pagination.component';
import { DialogService } from '@shared/services/dialog/dialog.service';
import { LessorApartmentService } from '../../services/lessor-apartment.service';
import { PaginationParams } from '@shared/models/pagination-params.model';
import { MiniLoadComponent } from '@shared/components/mini-load/mini-load.component';
import { ApartmentInsertComponent } from '@features/apartment/components/apartment-insert/apartment-insert.component';
import { ApartmentViewComponent } from '@features/apartment/components/apartment-view/apartment-view.component';
import { ApartmentUpdateComponent } from '@features/apartment/components/apartment-update/apartment-update.component';
import { ToastService } from '@shared/services/toast/toast.service';

@Component({
  selector: 'app-lessor-apartment',
  standalone: true,
  imports: [
    MatTableModule,
    MatSortModule,
    MatButtonModule,
    MatMenuModule,
    PaginationComponent,
    ApartmentViewComponent,
    ApartmentUpdateComponent,
    SearchComponent,
    RouterLink,
    CommonModule,
    MiniLoadComponent,
    ApartmentInsertComponent,
  ],
  templateUrl: './lessor-apartment.component.html',
  styleUrl: './lessor-apartment.component.scss',
})
export class LessorApartmentComponent implements OnInit {
  displayedColumns: string[] = [
    'name',
    'category',
    'address',
    'area',
    'price',
    'condition',
    'optional',
  ];
  dataSource = new MatTableDataSource<Room>();
  paginationParams = signal<PaginationParams>({
    pageSize: 0,
    count: 0,
    pageIndex: 1,
  });
  room!: Room;
  condition = Condition;
  category = Category;

  @ViewChild('viewModal', { read: TemplateRef })
  viewModal?: TemplateRef<any>;

  @ViewChild('updateModal', { read: TemplateRef })
  updateModal?: TemplateRef<any>;

  @ViewChild(MatSort) sort?: MatSort;

  dialogService = inject(DialogService);
  toastService = inject(ToastService);
  lessorApartmentService = inject(LessorApartmentService);

  async ngOnInit() {
    await this.init();
  }

  async init() {
    await this.lessorApartmentService.loadData(true);
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
    this.room = room;
    if (this.viewModal) {
      this.dialogService.view({
        title: 'Thông tin phòng',
        content: this.viewModal,
      });
    }
  }

  onUpdate(room: Room) {
    this.room = room;
    this.dialogService.form({
      title: 'Cập nhật phòng',
      content: this.updateModal,
      button: {
        accept: 'Cập nhật',
        decline: 'Hủy bỏ',
      },
    });
  }

  onDelete(idRoom: string) {
    this.dialogService
      .confirm({
        title: 'Xác nhận xóa phòng trọ',
        content: 'Bạn có chắc muốn xóa phòng trọ này không?',
        button: {
          accept: 'Xóa',
          decline: 'Hủy bỏ',
        },
      })
      .then(async () => {
        debugger
        const response = await this.lessorApartmentService.delete(idRoom);
        if (response.success) {
          this.toastService.success('Xóa bản phòng thành công!');
          await this.init();
        }
      });
  }
}
