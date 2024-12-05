import { User } from '@core/models/user.model';
import { Room } from '@features/apartment/models/room.model';

export type Post = {
  id: string;
  roomId: string;
  landlordId: string;
  title: string;
  description: string;
  availableFrom: Date;
  isAccept?: string;
  isHide: boolean;
  status?: string;
  createdAt?: Date;
  updatedAt?: Date;
  
  landlord?: User;
  room?: Room;
};

export const IsAccept: { [key: string]: string } = {
  Pending: 'Đang chờ xử lý',
  Accepted: 'Cho phép',
  Rejected: 'Từ chối',
  Canceled: 'Đã hủy',
};

export const StatusPost: { [key: string]: string } = {
  Published: 'Đã tải lên',
  Deleted: 'Đã gỡ',
};
