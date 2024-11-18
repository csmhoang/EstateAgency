import { User } from '@core/models/user.model';
import { Room } from '@features/apartment/models/room.model';

export type Post = {
  id: string;
  roomId: string;
  landlordId: string;
  title: string;
  description: string;
  availableFrom: Date;
  isAccept: string;
  status: string;
  room?: Room;
  landlord?: User;
  createdAt?: Date;
  updatedAt?: Date;
};

export const IsAccept: { [key: string]: string } = {
  Pending: 'Đang chờ xử lý',
  Accepted: 'Cho phép',
  Rejected: 'Từ chối',
};

export const StatusPost: { [key: string]: string } = {
  Published: 'Đã tải lên',
  Deleted: 'Đã gỡ',
};
