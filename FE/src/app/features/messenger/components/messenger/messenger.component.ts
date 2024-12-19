import { CommonModule } from '@angular/common';
import {
  Component,
  DestroyRef,
  inject,
  Input,
  OnInit,
  signal,
} from '@angular/core';
import { UserService } from '@core/services/user.service';
import { ConversationComponent } from '../conversation/conversation.component';
import { BoxChatComponent } from '../box-chat/box-chat.component';
import { Conversation } from '@features/messenger/models/conversation.model';
import { PresenceService } from '@core/services/presence.service';

@Component({
  selector: 'app-messenger',
  standalone: true,
  imports: [CommonModule, ConversationComponent, BoxChatComponent],
  templateUrl: './messenger.component.html',
  styleUrl: './messenger.component.scss',
})
export class MessengerComponent implements OnInit {
  @Input() data?: string;
  destroyRef = inject(DestroyRef);

  user = this.userService.currentUser;
  conversations = this.presenceService.conversationThread;
  conversation = signal<Conversation | null>(null);

  constructor(
    private userService: UserService,
    private presenceService: PresenceService
  ) {}

  ngOnInit() {
    if (this.data && this.user()) {
      this.presenceService
        .getConversation(this.user()!.id, this.data)
        .then((response) => {
          this.conversation.set(response.data);
        });
    }
  }

  conversationChanged(conversation: Conversation) {
    this.conversation.set(conversation);
  }
}
