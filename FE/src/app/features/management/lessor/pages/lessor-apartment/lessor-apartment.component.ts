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
import { Room } from '@features/apartment/models/room.model';
import { MaintenanceFormComponent } from '@features/maintenance/components/maintenance-form/maintenance-form.component';
import { SearchComponent } from '@shared/components/form/search/search.component';
import { PaginationComponent } from '@shared/components/pagination/pagination.component';
import { DialogService } from '@shared/services/dialog/dialog.service';
import { ToastService } from '@shared/services/toast/toast.service';
import { LessorApartmentService } from '../../services/lessor-apartment.service';
import { PaginationParams } from '@shared/models/pagination-params.model';

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
    RouterLink,
    CommonModule,
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
  paginationParams = signal<PaginationParams | null>(null);

  @ViewChild('maintenance', { read: TemplateRef })
  maintenance?: TemplateRef<any>;
  @ViewChild(MatSort) sort?: MatSort;

  // dialogService = inject(DialogService);
  // toastService = inject(ToastService);
  lessorApartmentService = inject(LessorApartmentService);

  async ngOnInit() {
    await this.lessorApartmentService.loadData();
    const page = this.lessorApartmentService.page();
    if (page) {
      this.paginationParams.set(page);
      this.dataSource.data = page.data;
      if (this.sort) {
        this.dataSource.sort = this.sort;
      }
    }
  }

  onPageChange(pageIndex: number) {
    console.log(pageIndex)
    // this.router.navigate([], {
    //   queryParams: { pageIndex },
    //   queryParamsHandling: 'merge',
    // });
    // this.specParams.pageIndex = pageIndex;
  }

  onView() {}

  onUpdate() {}

  onDelete() {}
}
