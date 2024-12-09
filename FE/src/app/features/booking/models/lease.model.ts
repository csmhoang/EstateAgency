import { LeaseDetail } from './lease-detail.model';

export type Lease = {
  id: string;
  bookingId: string;
  lessor: string;
  lessee: string;
  terms?: string;
  signedDate?: Date;
  status?: string;
  createdAt?: Date;
  updatedAt?: Date;
  
  leaseDetails?: LeaseDetail[];
};

export const StatusLease: { [key: string]: string } = {
  Pending: 'Đã gửi',
  Active: 'Có hiệu lực',
  Expired: 'Hết hạn',
  Canceled: "Đã hủy"
};