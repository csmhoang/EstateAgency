export type Booking = {
  id?: string;
  postId: string;
  tenantId: string;
  intendedIntoDate: Date;
  numberOfTenant: number;
  note?: string;
  status: string;
  createdAt?: Date;
  updatedAt?: Date;
};

export const StatusBooking: { [key: string]: string } = {
  Pending: 'Đang chờ xử lý',
  Confirmed: 'Xác nhận',
  Rejected: 'Từ chối',
  Canceled: 'Đã hủy',
};
