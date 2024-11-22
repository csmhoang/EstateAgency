import { Component, computed, Input, signal } from '@angular/core';
import { LessorItemComponent } from '../lessor-item/lessor-item.component';
import { User } from '@core/models/user.model';

@Component({
  selector: 'app-lessor-list',
  standalone: true,
  imports: [LessorItemComponent],
  templateUrl: './lessor-list.component.html',
  styleUrl: './lessor-list.component.scss',
})
export class LessorListComponent {
  @Input()
  lessors?: User[];
  size = signal<number>(3);
  data = computed(() => this.lessors?.slice(0, this.size()));

  viewMore() {
    this.size.update((v) => v + 3);
  }
}
