import { Component } from '@angular/core';
import { SearchComponent } from '@shared/components/form/search/search.component';

@Component({
  selector: 'app-lessor-filter',
  standalone: true,
  imports: [SearchComponent],
  templateUrl: './lessor-filter.component.html',
  styleUrl: './lessor-filter.component.scss',
})
export class LessorFilterComponent {}
