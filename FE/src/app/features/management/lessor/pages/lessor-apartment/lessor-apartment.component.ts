import {
  AfterViewInit,
  Component,
  inject,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { RouterLink } from '@angular/router';
import { MaintenanceFormComponent } from '@features/maintenance/components/maintenance-form/maintenance-form.component';
import { SearchComponent } from '@shared/components/form/search/search.component';
import { PaginationComponent } from '@shared/components/pagination/pagination.component';
import { DialogService } from '@shared/services/dialog/dialog.service';
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
    MaintenanceFormComponent,
    SearchComponent,
    RouterLink
  ],
  templateUrl: './lessor-apartment.component.html',
  styleUrl: './lessor-apartment.component.scss',
})
export class LessorApartmentComponent implements AfterViewInit {
  @ViewChild('maintenance', { read: TemplateRef })
  maintenance?: TemplateRef<any>;

  dialogService = inject(DialogService);
  toastService = inject(ToastService);

  displayedColumns: string[] = [
    'name',
    'category',
    'address',
    'area',
    'price',
    'condition',
    'disabled',
  ];
  
  dataSource = new MatTableDataSource<PeriodicElement>(ELEMENT_DATA);

  @ViewChild(MatSort) sort?: MatSort;

  ngAfterViewInit() {
    if (this.sort) {
      this.dataSource.sort = this.sort;
    }
  }

  onToast() {
    this.toastService.success('Lỗi 404');
  }

  onForm() {
    this.dialogService.form({
      title: 'Thông báo xác nhận',
      content: this.maintenance,
      button: {
        accept: 'Đồng ý',
        decline: 'Hủy bỏ',
      },
    });
  }
}
