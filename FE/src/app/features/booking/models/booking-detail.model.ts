import { Room } from '@features/apartment/models/room.model';
import { Booking } from './booking.model';

export type BookingDetail = {
  id: string;
  roomId: string;
  bookingId: string;
  numberOfMonth: number;
  numberOfTenant: number;
  price: number;
  rejectionReason?: string;
  status?: string;
  createdAt?: Date;
  updatedAt?: Date;
  
  room?: Room;
  booking?: Booking;
};

export const StatusBookingDetail: { [key: string]: string } = {
  Pending: 'Đang chờ xử lý',
  Accepted: 'Chấp nhận',
  Rejected: 'Từ chối',
  Canceled: "Đã hủy"
};
