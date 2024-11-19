export type Reservation = {
  id?: string;
  tenantId: string;
  postId: string;
  note?: string;
  rejectionReason?: string;
  reservationDate: string;
  status: string;
  createdAt?: Date;
  updatedAt?: Date;
};

export const StatusReservation: { [key: string]: string } = {
  Pending: 'Đang chờ xử lý',
  Confirmed: 'Xác nhận',
  Rejected: 'Từ chối',
  Canceled: 'Đã hủy',
};
