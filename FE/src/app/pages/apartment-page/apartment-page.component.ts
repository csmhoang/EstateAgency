import { Component } from '@angular/core';
import { ApartmentFilterComponent } from '@features/apartment/components/apartment-filter/apartment-filter.component';
import { FooterComponent } from '@features/footer/footer.component';
import { HeaderComponent } from '@features/header/header.component';
import { PostItemComponent } from '@features/post/components/post-item/post-item.component';
import { AutoCompleteComponent } from '@shared/components/auto-complete/auto-complete.component';
import { PaginationComponent } from '@shared/components/pagination/pagination.component';
import { ScrollTopComponent } from '@shared/components/scroll-top/scroll-top.component';

@Component({
  selector: 'app-apartment-page',
  standalone: true,
  imports: [
    HeaderComponent,
    FooterComponent,
    ScrollTopComponent,
    PostItemComponent,
    AutoCompleteComponent,
    ApartmentFilterComponent,
    PaginationComponent,
  ],
  templateUrl: './apartment-page.component.html',
  styleUrl: './apartment-page.component.scss',
})
export class ApartmentPageComponent {}
