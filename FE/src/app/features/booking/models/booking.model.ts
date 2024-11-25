import { Room } from "@features/apartment/models/room.model";

export type Booking = {
  id?: string;
  roomId: string;
  tenantId: string;
  invoiceId: string;
  intendedIntoDate: Date;
  endDate: Date;
  numberOfTenant: number;
  note?: string;
  rejectionReason?: string;
  status?: string;
  createdAt?: Date;
  updatedAt?: Date;
  room?: Room;
};

export const StatusBooking: { [key: string]: string } = {
  Pending: 'Đang chờ xử lý',
  Accepted: 'Chấp nhận',
  Rejected: 'Từ chối',
  Canceled: 'Đã hủy',
};
