import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-heart-like',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './heart-like.component.html',
  styleUrl: './heart-like.component.scss',
})
export class HeartLikeComponent {
  isLiked = false;

  toggleLike() {
    this.isLiked = !this.isLiked;
  }
}
