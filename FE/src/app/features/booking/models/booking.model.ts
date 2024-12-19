import { Invoice } from './invoice.model';
import { User } from '@core/models/user.model';
import { BookingDetail } from './booking-detail.model';
import { Lease } from './lease.model';

export type Booking = {
  id: string;
  tenantId: string;
  invoiceId: string;
  note?: string;
  status?: string;
  createdAt?: Date;
  updatedAt?: Date;
  
  invoice?: Invoice;
  tenant?: User;
  lease?: Lease;
  bookingDetails?: BookingDetail[];
};

export const StatusBooking: { [key: string]: string } = {
  Pending: 'Đang chờ xử lý',
  Canceled: 'Đã hủy',
  Confirmed: 'Đã xác nhận',
  Rejected: "Đã từ chối"
};
