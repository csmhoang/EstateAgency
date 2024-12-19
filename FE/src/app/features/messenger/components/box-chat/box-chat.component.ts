import {
  Component,
  Input,
  OnChanges,
  OnDestroy,
  OnInit,
  SimpleChanges,
} from '@angular/core';
import { MessageComponent } from '../message/message.component';
import { UserService } from '@core/services/user.service';
import { MessageService } from '@features/messenger/services/message.service';
import { FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { Message } from '@features/messenger/models/message.model';
import { Conversation } from '@features/messenger/models/conversation.model';
import { CommonModule } from '@angular/common';
import { User } from '@core/models/user.model';

@Component({
  selector: 'app-box-chat',
  standalone: true,
  imports: [MessageComponent, ReactiveFormsModule, CommonModule],
  templateUrl: './box-chat.component.html',
  styleUrl: './box-chat.component.scss',
})
export class BoxChatComponent implements OnChanges, OnDestroy {
  @Input() conversation?: Conversation | null;

  user = this.userService.currentUser();
  receiver?: User;

  message = new FormControl('', [Validators.required]);
  messages = this.messageService.messageThread;

  constructor(
    private userService: UserService,
    private messageService: MessageService
  ) {}

  ngOnChanges() {
    if (this.conversation) {
      this.receiver = this.conversation.participants?.find(
        (p) => p.userId !== this.user?.id
      )?.user;
      this.messageService.stopHubConnection();
      this.messageService.createHubConnection(this.conversation.id);
    }
  }

  async sendMessage() {
    if (this.message.valid && this.conversation && this.user) {
      const message: Message = {
        senderId: this.user.id,
        receiverId: this.receiver?.id!,
        conversationId: this.conversation.id,
        content: this.message.value!,
      };
      await this.messageService.sendMessage(message);
      this.message.reset();
    }
  }

  ngOnDestroy() {
    this.messageService.stopHubConnection();
  }
}
