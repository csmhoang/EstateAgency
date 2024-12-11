import { User } from '@core/models/user.model';

export type Message = {
  id?: string;
  senderId: string;
  receiverId: string;
  conversationId: string;
  content: string;
  sentAt?: Date;

  receiver?: User;
  sender?: User;
};
