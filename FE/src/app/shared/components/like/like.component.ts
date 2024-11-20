import { CommonModule } from '@angular/common';
import { Component, Input, output, signal } from '@angular/core';

@Component({
  selector: 'app-like',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './like.component.html',
  styleUrl: './like.component.scss',
})
export class LikeComponent {
  @Input() isSaved?: boolean;
  save = output<boolean>();

  toggleLike() {
    this.isSaved = !this.isSaved;
    this.save.emit(this.isSaved);
  }
}
