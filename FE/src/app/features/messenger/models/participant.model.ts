import { User } from '@core/models/user.model';
import { Conversation } from './conversation.model';

export type Participant = {
  id: string;
  userId: string;
  conversationId: string;

  user?: User;
  conversation?: Conversation;
};
