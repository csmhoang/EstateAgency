import { Component } from '@angular/core';
import { FooterComponent } from '@core/layout/footer/footer.component';
import { HeaderComponent } from '@core/layout/header/header.component';
import { LessorFilterComponent } from '@features/lessor/components/lessor-filter/lessor-filter.component';
import { LessorListComponent } from '@features/lessor/components/lessor-list/lessor-list.component';

@Component({
  selector: 'app-lessor',
  standalone: true,
  imports: [
    HeaderComponent,
    FooterComponent,
    LessorListComponent,
    LessorFilterComponent,
  ],
  templateUrl: './lessor.component.html',
  styleUrl: './lessor.component.scss',
})
export class LessorComponent {}
