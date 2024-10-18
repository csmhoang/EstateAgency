import { Component, ViewEncapsulation } from '@angular/core';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-pagination',
  standalone: true,
  imports: [NgbPaginationModule],
  templateUrl: './pagination.component.html',
  styleUrl: './pagination.component.scss',
  encapsulation: ViewEncapsulation.None,
})
export class PaginationComponent {
  page = 4;
}
