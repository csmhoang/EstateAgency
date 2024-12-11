import { Component, Input, OnInit, output, signal } from '@angular/core';
import { User } from '@core/models/user.model';
import { PresenceService } from '@core/services/presence.service';
import { UserService } from '@core/services/user.service';
import { Conversation } from '@features/messenger/models/conversation.model';
import { Participant } from '@features/messenger/models/participant.model';

@Component({
  selector: 'app-conversation',
  standalone: true,
  imports: [],
  templateUrl: './conversation.component.html',
  styleUrl: './conversation.component.scss',
})
export class ConversationComponent implements OnInit {
  @Input() conversation?: Conversation;
  isOnline = signal<boolean>(false);
  clickConversation = output<Conversation>();
  constructor(private presenceService: PresenceService) {}

  ngOnInit() {
    if (this.conversation) {
      this.isOnline.set(
        this.presenceService
          .onlineUsers()
          .includes(this.conversation.receiver?.username!)
      );
    }
  }

  onConversation(conversation: Conversation) {
    this.clickConversation.emit(conversation);
  }
}
