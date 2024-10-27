import {
  Component,
  input,
  OnChanges,
  output,
  SimpleChanges,
  ViewEncapsulation,
} from '@angular/core';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { PaginationParams } from '@shared/models/pagination-params.model';

@Component({
  selector: 'app-pagination',
  standalone: true,
  imports: [NgbPaginationModule],
  templateUrl: './pagination.component.html',
  styleUrl: './pagination.component.scss',
  encapsulation: ViewEncapsulation.None,
})
export class PaginationComponent implements OnChanges {
  paginationParams = input<PaginationParams | null>();
  pageChange = output<number>();
  pageSize: number = 0;
  pageIndex: number = 0;
  count: number = 0;

  ngOnChanges(changes: SimpleChanges): void {
    if(changes["paginationParams"] && this.paginationParams()){
      this.pageSize = this.paginationParams()?.pageSize || 0;
      this.count = this.paginationParams()?.count || 0;
    }
  }

  onPageChange() {
    this.pageChange.emit(this.pageIndex);
  }
}
