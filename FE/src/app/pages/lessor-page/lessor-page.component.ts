import { Component } from '@angular/core';
import { FooterComponent } from '@features/footer/footer.component';
import { HeaderComponent } from '@features/header/header.component';
import { LessorFilterComponent } from '@features/lessor/components/lessor-filter/lessor-filter.component';
import { LessorListComponent } from '@features/lessor/components/lessor-list/lessor-list.component';
import { PaginationComponent } from '@shared/components/pagination/pagination.component';
import { ScrollTopComponent } from '@shared/components/scroll-top/scroll-top.component';

@Component({
  selector: 'app-lessor-page',
  standalone: true,
  imports: [
    HeaderComponent,
    LessorListComponent,
    FooterComponent,
    ScrollTopComponent,
    PaginationComponent,
    LessorFilterComponent,
  ],
  templateUrl: './lessor-page.component.html',
  styleUrl: './lessor-page.component.scss',
})
export class LessorPageComponent {}
