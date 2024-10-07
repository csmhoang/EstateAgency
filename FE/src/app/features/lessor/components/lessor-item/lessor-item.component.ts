import { Component } from '@angular/core';
import { LikeComponent } from '@shared/components/like/like.component';

@Component({
  selector: 'app-lessor-item',
  standalone: true,
  imports: [LikeComponent],
  templateUrl: './lessor-item.component.html',
  styleUrl: './lessor-item.component.scss',
})
export class LessorItemComponent {}
