import { Component } from '@angular/core';
import { ApartmentFilterComponent } from '@features/apartment/components/apartment-filter/apartment-filter.component';
import { PostRootListComponent } from '@features/post/components/post-root-list/post-root-list.component';
import { AutoCompleteComponent } from '@shared/components/auto-complete/auto-complete.component';

@Component({
  selector: 'app-apartment-page',
  standalone: true,
  imports: [
    AutoCompleteComponent,
    ApartmentFilterComponent,
    PostRootListComponent,
  ],
  templateUrl: './apartment-page.component.html',
  styleUrl: './apartment-page.component.scss',
})
export class ApartmentPageComponent {}
