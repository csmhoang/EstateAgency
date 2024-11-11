import { Component, Input } from '@angular/core';
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
  lessors!: User[];
}
