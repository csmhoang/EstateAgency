import { LeaseDetail } from './lease-detail.model';

export type Lease = {
  id: string;
  bookingId: string;
  lessor: string;
  lessee: string;
  terms: string;
  startDate: Date;
  signedDate?: Date;
  status?: string;
  createdAt?: Date;
  updatedAt?: Date;
  leaseDetails?: LeaseDetail[];
};

export const StatusLease: { [key: string]: string } = {
  Pending: 'Đã gửi',
  Confirmed: "Xác nhận",
  Active: 'Có hiệu lực',
  Expired: 'Hết hạn',
  Rejected: "Đã từ chối"
};