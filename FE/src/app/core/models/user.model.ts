import { Room } from '@features/apartment/models/room.model';
import { UserRole } from './user-role.model';
import { Post } from '@features/post/models/post.model';
import { SavePost } from '@features/post/models/save-post.model';
import { Follow } from './follow.model';
import { Reservation } from '@features/reservation/models/reservation.model';
import { Booking } from '@features/booking/models/booking.model';
import { Cart } from '@features/Cart/models/cart.model';

export type User = {
  id: string;
  fullName: string;
  avatarUrl?: string;
  publicId?: string;
  phoneNumber: string;
  username: string;
  email: string;
  numberOfFollowers?: number;
  gender?: string;
  dateOfBirth?: Date;
  address: string;
  description?: string;
  createdAt?: Date;
  updatedAt?: Date;

  cart?: Cart;
  userRoles?: UserRole[];
  posts?: Post[];
  rooms?: Room[];
  savePosts?: SavePost[];
  followers?: Follow[];
  followees?: Follow[];
  reservations?: Reservation[];
  bookings?: Booking[];
};

export const Gender: { [key: string]: string } = {
  Male: 'Nam',
  Female: 'Nữ',
  Other: 'Khác',
};
