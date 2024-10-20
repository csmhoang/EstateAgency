import { Component } from '@angular/core';
import { PaginationComponent } from '@shared/components/pagination/pagination.component';
import { LessorItemComponent } from '../lessor-item/lessor-item.component';

@Component({
  selector: 'app-lessor-list',
  standalone: true,
  imports: [LessorItemComponent, PaginationComponent],
  templateUrl: './lessor-list.component.html',
  styleUrl: './lessor-list.component.scss',
})
export class LessorListComponent {}
