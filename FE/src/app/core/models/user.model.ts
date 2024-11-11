import { Room } from '@features/apartment/models/room.model';
import { UserRole } from './user-role.model';

export type User = {
  id: string;
  fullName: string;
  avatarUrl?: string;
  publicId?: string;
  phoneNumber: string;
  email: string;
  numberOfFollowers?: number;
  gender?: string;
  dateOfBirth?: Date;
  address: string;
  createdAt?: Date;
  updatedAt?: Date;
  description?: string;
  userRoles?: UserRole[];
  rooms?: Room[];
};

export const Gender: { [key: string]: string } = {
  Male: 'Nam',
  Female: 'Nữ',
  Other: 'Khác',
};
