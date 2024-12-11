import { User } from '@core/models/user.model';
import { Message } from './message.model';
import { Participant } from './participant.model';

export type Conversation = {
  id: string;
  
  receiver?: User;
  participants?: Participant[];
  messages?: Message[];
};
