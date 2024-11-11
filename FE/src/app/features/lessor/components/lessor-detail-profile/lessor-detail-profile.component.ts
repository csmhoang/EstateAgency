import { Component, Input, OnInit } from '@angular/core';
import { User } from '@core/models/user.model';

@Component({
  selector: 'app-lessor-detail-profile',
  standalone: true,
  imports: [],
  templateUrl: './lessor-detail-profile.component.html',
  styleUrl: './lessor-detail-profile.component.scss',
})
export class LessorDetailProfileComponent implements OnInit {
  @Input()
  lessor?: User;
  numberOfPost?: number;

  timeSinceParticipateFilter(time: Date) {
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

  ngOnInit() {
    this.numberOfPost = this.lessor?.rooms?.reduce((sum, room) => {
      if (room.posts) {
        return sum + room.posts.length;
      }
      return sum;
    }, 0);
  }
}
