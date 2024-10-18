import { AfterViewInit, Component, inject, ViewChild } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { PaginationComponent } from '@shared/components/pagination/pagination.component';
import { DialogService } from '@shared/services/dialog/dialog.service';

export interface PeriodicElement {
  name: string;
  position: number;
  weight: number;
  symbol: string;
}
const ELEMENT_DATA: PeriodicElement[] = [
  { position: 1, name: 'Hydrogen', weight: 1.0079, symbol: 'H' },
  { position: 2, name: 'Helium', weight: 4.0026, symbol: 'He' },
  { position: 3, name: 'Lithium', weight: 6.941, symbol: 'Li' },
  { position: 4, name: 'Beryllium', weight: 9.0122, symbol: 'Be' },
  { position: 5, name: 'Boron', weight: 10.811, symbol: 'B' },
  { position: 6, name: 'Carbon', weight: 12.0107, symbol: 'C' },
  { position: 7, name: 'Nitrogen', weight: 14.0067, symbol: 'N' },
  { position: 8, name: 'Oxygen', weight: 15.9994, symbol: 'O' },
  { position: 9, name: 'Fluorine', weight: 18.9984, symbol: 'F' },
  { position: 10, name: 'Neon', weight: 20.1797, symbol: 'Ne' },
];
@Component({
  selector: 'app-rented-history',
  standalone: true,
  imports: [
    MatTableModule,
    MatSortModule,
    MatButtonModule,
    MatMenuModule,
    PaginationComponent,
  ],
  templateUrl: './rented-history.component.html',
  styleUrl: './rented-history.component.scss',
})
export class RentedHistoryComponent implements AfterViewInit {
  dialogService = inject(DialogService);

  displayedColumns: string[] = [
    'name',
    'lessor',
    'phoneNumber',
    'price',
    'duration',
    'status',
    'disabled',
  ];
  dataSource = new MatTableDataSource<PeriodicElement>(ELEMENT_DATA);

  @ViewChild(MatSort) sort?: MatSort;

  ngAfterViewInit() {
    if (this.sort) {
      this.dataSource.sort = this.sort;
    }
  }
  onConfirm() {
    this.dialogService.confirm({
      title: 'Thông báo xác nhận',
      content: 'Bạn có chắc muốn xóa không!',
      button: {
        accept: 'Đồng ý',
        decline: 'Hủy bỏ',
      },
    });
  }
}
