import { User } from "./user.model";

export type Follow = {
  id?: string;
  followerId: string;
  followeeId: string;
  createdAt?: Date;
  updatedAt?: Date;
  
  follower?: User;
  followee?: User;
};
