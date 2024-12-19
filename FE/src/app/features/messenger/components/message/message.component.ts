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
}
