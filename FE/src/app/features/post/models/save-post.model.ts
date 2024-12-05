import { Post } from './post.model';

export type SavePost = {
  id?: string;
  userId: string;
  postId: string;
  createdAt?: Date;
  updatedAt?: Date;
  
  post?: Post;
};
