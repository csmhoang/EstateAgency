import { Role } from './role.model';
import { User } from './user.model';

export type UserRole = {
  user?: User;
  role?: Role;
};
