import { CommonModule } from '@angular/common';
import {
  Component,
  computed,
  DestroyRef,
  inject,
  Input,
  OnInit,
  output,
} from '@angular/core';
import { User } from '@core/models/user.model';
import { PresenceService } from '@core/services/presence.service';
import { UserService } from '@core/services/user.service';
import { Conversation } from '@features/messenger/models/conversation.model';

@Component({
  selector: 'app-conversation',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './conversation.component.html',
  styleUrl: './conversation.component.scss',
})
export class ConversationComponent implements OnInit {
  @Input() conversation?: Conversation;
  
  destroyRef = inject(DestroyRef);
  conversationChanged = output<Conversation>();
  
  user = this.userService.currentUser();
  receiver?: User;
  isOnline = computed(() => {
    if (this.receiver) {
      return this.presenceService
        .onlineUsers()
        .includes(this.receiver.id);
    }
    return false;
  });

  constructor(
    private userService: UserService,
    private presenceService: PresenceService
  ) {}

  ngOnInit() {
    if (this.conversation) {
      this.receiver = this.conversation.participants?.find(
        (p) => p.userId !== this.user?.id
      )?.user;
    }
  }

  onConversation(conversation: Conversation) {
    this.conversationChanged.emit(conversation);
  }
}
