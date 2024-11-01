import { Room } from "@features/apartment/models/room.model";

export type Post = {
  id: string;
  postCode: string;
  roomId: string;
  title: string;
  description: string;
  availableFrom: Date;
  isAccept: string;
  status: string;
  room?: Room;
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
