import { Invoice } from './invoice.model';
import { User } from '@core/models/user.model';
import { BookingDetail } from './booking-detail.model';

export type Booking = {
  id?: string;
  tenantId: string;
  invoiceId: string;
  note?: string;
  status?: string;
  createdAt?: Date;
  updatedAt?: Date;
  invoice?: Invoice;
  tenant?: User;
  bookingDetails?: BookingDetail[];
};

export const StatusBooking: { [key: string]: string } = {
  Pending: 'Đang chờ xử lý',
  Confirmed: 'Đã xác nhận',
  Canceled: 'Đã hủy',
};
