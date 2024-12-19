import { Message } from './message.model';
import { Participant } from './participant.model';

export type Conversation = {
  id: string;
  
  participants?: Participant[];
  messages?: Message[];
};
