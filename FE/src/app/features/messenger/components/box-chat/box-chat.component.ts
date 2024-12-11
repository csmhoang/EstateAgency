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

@Component({
  selector: 'app-box-chat',
  standalone: true,
  imports: [MessageComponent, ReactiveFormsModule],
  templateUrl: './box-chat.component.html',
  styleUrl: './box-chat.component.scss',
})
export class BoxChatComponent implements OnChanges, OnDestroy {
  @Input() conversation?: Conversation | null;
  user = this.userService.currentUser();
  messages = this.messageService.messageThread;

  message = new FormControl('', [Validators.required]);

  constructor(
    private userService: UserService,
    private messageService: MessageService
  ) {}
  ngOnChanges() {
    if (this.conversation) {
      this.messageService.stopHubConnection();
      this.messageService.createHubConnection(this.conversation.id);
    }
  }

  async sendMessage() {
    if (this.message.valid && this.conversation && this.user) {
      const message: Message = {
        senderId: this.user.id,
        receiverId: this.conversation.receiver?.id!,
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
