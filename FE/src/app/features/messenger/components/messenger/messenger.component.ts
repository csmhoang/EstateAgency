import { CommonModule } from '@angular/common';
import {
  Component,
  DestroyRef,
  inject,
  Input,
  OnInit,
  signal,
} from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { UserService } from '@core/services/user.service';
import { ConversationService } from '@features/messenger/services/conversation.service';
import { MessageService } from '@features/messenger/services/message.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { catchError, map, Observable, of } from 'rxjs';
import { ConversationComponent } from '../conversation/conversation.component';
import { BoxChatComponent } from '../box-chat/box-chat.component';
import { Conversation } from '@features/messenger/models/conversation.model';

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
  activeModal = inject(NgbActiveModal);
  user = this.userService.currentUser();
  conversations?: Observable<Conversation[] | null>;
  conversation = signal<Conversation | null>(null);

  constructor(
    private conversationService: ConversationService,
    private userService: UserService
  ) {}

  ngOnInit() {
    this.conversations = this.conversationService.getAllOfCurrent(true).pipe(
      map((conversations) =>
        conversations.map((conversation) => ({
          ...conversation,
          receiver: conversation.participants?.find(
            (p) => p.userId !== this.user?.id
          )?.user,
        }))
      ),
      takeUntilDestroyed(this.destroyRef),
      catchError(() => of(null))
    );

    if (this.data) {
      this.conversationService
        .getTwoUserId(this.data)
        .pipe(
          takeUntilDestroyed(this.destroyRef),
          catchError(() => of(null))
        )
        .subscribe((response) => {
          if (response?.success) {
            this.conversation.set(response.data);
          }
        });
    }
  }

  selectConversation(conversation: Conversation) {
    this.conversation.set(conversation);
  }

  accept() {}

  decline() {
    this.activeModal.dismiss(false);
  }
}
