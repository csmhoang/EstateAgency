import { Post } from "@features/post/models/post.model";

export type Reservation = {
  id: string;
  tenantId: string;
  postId: string;
  note?: string;
  rejectionReason?: string;
  reservationDate: Date;
  status: string;
  createdAt?: Date;
  updatedAt?: Date;
  post?: Post;
};

export const StatusReservation: { [key: string]: string } = {
  Pending: 'Đang chờ xử lý',
  Confirmed: 'Xác nhận',
  Rejected: 'Từ chối',
  Canceled: 'Đã hủy',
};
