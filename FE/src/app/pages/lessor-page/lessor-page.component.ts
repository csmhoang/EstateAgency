import { Component } from '@angular/core';
import { LessorFilterComponent } from '@features/lessor/components/lessor-filter/lessor-filter.component';
import { LessorListComponent } from '@features/lessor/components/lessor-list/lessor-list.component';
import { PaginationComponent } from '@shared/components/pagination/pagination.component';

@Component({
  selector: 'app-lessor-page',
  standalone: true,
  imports: [LessorListComponent, PaginationComponent, LessorFilterComponent],
  templateUrl: './lessor-page.component.html',
  styleUrl: './lessor-page.component.scss',
})
export class LessorPageComponent {}
