import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { Category, Interior, Price, Room } from '@features/apartment/models/room.model';
import { Post } from '@features/post/models/post.model';
import { LikeComponent } from '@shared/components/like/like.component';

@Component({
  selector: 'app-apartment-primary-info',
  standalone: true,
  imports: [LikeComponent, CommonModule],
  templateUrl: './apartment-primary-info.component.html',
  styleUrl: './apartment-primary-info.component.scss',
})
export class ApartmentPrimaryInfoComponent {
  @Input() post!: Post;

  priceFilter = Price;
  categoryFilter = Category;
  interiorFilter = Interior;

  timeSinceUpdateFilter(time: Date) {
    if (this.post.updatedAt) {
      time = this.post.updatedAt;
    }
    const now = new Date();
    const seconds = Math.floor(
      (now.getTime() - new Date(time).getTime()) / 1000
    );

    const intervals: { [key: string]: number } = {
      năm: 31536000,
      tháng: 2592000,
      tuần: 604800,
      ngày: 86400,
      giờ: 3600,
      phút: 60,
      giây: 1,
    };

    for (const [unit, value] of Object.entries(intervals)) {
      const interval = Math.floor(seconds / value);
      if (interval >= 1) {
        return `${interval} ${unit} trước`;
      }
    }

    return 'Vừa xong';
  }
}
