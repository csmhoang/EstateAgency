import { LeaseDetail } from './lease-detail.model';

export type Lease = {
  id: string;
  tenantId: string;
  bookingId: string;
  lessor: string;
  lessee: string;
  terms: string;
  startDate: Date;
  signedDate: Date;
  status?: string;
  createdAt?: Date;
  updatedAt?: Date;
  leaseDetails?: LeaseDetail[];
};

export const StatusLease: { [key: string]: string } = {
  Pending: 'Đang chờ xử lý',
  Active: 'Có hiệu lực',
  Expired: 'Hết hạn',
  Canceled: "Đã hủy"
};