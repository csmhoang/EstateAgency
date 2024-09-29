import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-heart',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './heart.component.html',
  styleUrl: './heart.component.scss',
})
export class HeartComponent {
  isLiked = false;

  toggleLike() {
    this.isLiked = !this.isLiked;
  }
}
