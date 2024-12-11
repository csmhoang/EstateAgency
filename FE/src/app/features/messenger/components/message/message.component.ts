import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { UserService } from '@core/services/user.service';
import { Message } from '@features/messenger/models/message.model';

@Component({
  selector: 'app-message',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './message.component.html',
  styleUrl: './message.component.scss',
})
export class MessageComponent {
  @Input() message?: Message;
  user = this.userService.currentUser();

  constructor(private userService: UserService) {}

  timeSinceSendAtFilter(time: Date) {
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
