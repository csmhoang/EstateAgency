import { CommonModule } from '@angular/common';
import { Component, output, signal } from '@angular/core';

@Component({
  selector: 'app-like',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './like.component.html',
  styleUrl: './like.component.scss',
})
export class LikeComponent {
  isSaved = signal<boolean>(false);
  save = output<boolean>();

  toggleLike() {
    this.isSaved.update((v) => !v);
    this.save.emit(this.isSaved());
  }
}
