import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-like',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './like.component.html',
  styleUrl: './like.component.scss',
})
export class LikeComponent {
  isLiked = false;

  toggleLike() {
    this.isLiked = !this.isLiked;
  }
}
