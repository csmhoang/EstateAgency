import { Component } from '@angular/core';
import { FooterComponent } from '@core/layout/footer/footer.component';
import { HeaderComponent } from '@core/layout/header/header.component';
import { ApartmentFilterComponent } from '@features/apartment/components/apartment-filter/apartment-filter.component';
import { PostRootListComponent } from '@features/post/components/post-root-list/post-root-list.component';
import { AutoCompleteComponent } from '@shared/components/form/auto-complete/auto-complete.component';

@Component({
  selector: 'app-apartment',
  standalone: true,
  imports: [
    HeaderComponent,
    FooterComponent,
    AutoCompleteComponent,
    ApartmentFilterComponent,
    PostRootListComponent,
  ],
  templateUrl: './apartment.component.html',
  styleUrl: './apartment.component.scss',
})
export class ApartmentComponent {}
