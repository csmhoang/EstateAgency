import { Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { User } from '@core/models/user.model';
import { LikeComponent } from '@shared/components/like/like.component';

@Component({
  selector: 'app-lessor-item',
  standalone: true,
  imports: [LikeComponent, RouterLink],
  templateUrl: './lessor-item.component.html',
  styleUrl: './lessor-item.component.scss',
})
export class LessorItemComponent {
  @Input()
  lessor?: User;
}
