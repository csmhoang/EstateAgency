import { User } from '@core/models/user.model';

export type Feedback = {
  id: string;
  tenantId?: string;
  postId?: string;
  replyId?: string;
  rating: number;
  comment?: string;
  createdAt?: Date;
  tenant?: User;
  replies?: Feedback[];
};
