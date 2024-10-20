import { Component } from '@angular/core';
import { SearchComponent } from '@shared/components/form/search/search.component';

@Component({
  selector: 'app-apartment-filter',
  standalone: true,
  imports: [SearchComponent],
  templateUrl: './apartment-filter.component.html',
  styleUrl: './apartment-filter.component.scss',
})
export class ApartmentFilterComponent {}
