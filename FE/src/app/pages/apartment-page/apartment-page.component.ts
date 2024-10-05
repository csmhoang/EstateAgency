import { Component } from '@angular/core';
import { ApartmentFilterComponent } from '@features/apartment/components/apartment-filter/apartment-filter.component';
import { FooterComponent } from '@features/footer/footer.component';
import { HeaderComponent } from '@features/header/header.component';
import { PostRootListComponent } from '@features/post/components/post-root-list/post-root-list.component';
import { AutoCompleteComponent } from '@shared/components/auto-complete/auto-complete.component';
import { ScrollTopComponent } from '@shared/components/scroll-top/scroll-top.component';

@Component({
  selector: 'app-apartment-page',
  standalone: true,
  imports: [
    HeaderComponent,
    FooterComponent,
    ScrollTopComponent,
    AutoCompleteComponent,
    ApartmentFilterComponent,
    PostRootListComponent,
  ],
  templateUrl: './apartment-page.component.html',
  styleUrl: './apartment-page.component.scss',
})
export class ApartmentPageComponent {}
