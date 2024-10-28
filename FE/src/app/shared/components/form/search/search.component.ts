import { Component, input, output } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-search',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './search.component.html',
  styleUrl: './search.component.scss',
})
export class SearchComponent {
  placeholder = input<string>('');
  query = new FormControl('');
  search = output<string>();

  onSearch() {
    this.search.emit(this.query?.value || '');
  }
}
