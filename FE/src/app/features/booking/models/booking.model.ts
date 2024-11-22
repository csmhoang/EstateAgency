import { Post } from "@features/post/models/post.model";

export type Booking = {
  id?: string;
  postId: string;
  tenantId: string;
  intendedIntoDate: Date;
  endDate: Date;
  numberOfTenant: number;
  note?: string;
  rejectionReason?: string;
  status: string;
  createdAt?: Date;
  updatedAt?: Date;
  post?: Post;
};

export const StatusBooking: { [key: string]: string } = {
  Pending: 'Đang chờ xử lý',
  Confirmed: 'Xác nhận',
  Rejected: 'Từ chối',
  Canceled: 'Đã hủy',
};
