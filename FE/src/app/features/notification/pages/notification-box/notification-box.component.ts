import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { PresenceService } from '@core/services/presence.service';
import { Notice } from '@features/notification/models/notification.model';

@Component({
  selector: 'app-notification-box',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './notification-box.component.html',
  styleUrl: './notification-box.component.scss',
})
export class NotificationBoxComponent {
  @Input() notifications?: Notice[];

  constructor(private presenceService: PresenceService) {}

  async onDelete(notificationId: string) {
    await this.presenceService.deleteNotification(notificationId);
  }
}
