import { Component } from '@angular/core';
import { FooterComponent } from '@features/footer/footer.component';
import { HeaderComponent } from '@features/header/header.component';
import { RenterFilterComponent } from '@features/renter/components/renter-filter/renter-filter.component';
import { RenterListComponent } from '@features/renter/components/renter-list/renter-list.component';
import { PaginationComponent } from '@shared/components/pagination/pagination.component';
import { ScrollTopComponent } from '@shared/components/scroll-top/scroll-top.component';

@Component({
  selector: 'app-renter-page',
  standalone: true,
  imports: [
    HeaderComponent,
    RenterListComponent,
    FooterComponent,
    ScrollTopComponent,
    PaginationComponent,
    RenterFilterComponent,
  ],
  templateUrl: './renter-page.component.html',
  styleUrl: './renter-page.component.scss',
})
export class RenterPageComponent {}
