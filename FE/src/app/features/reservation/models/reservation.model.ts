import { User } from "@core/models/user.model";
import { Room } from "@features/apartment/models/room.model";

export type Reservation = {
  id: string;
  tenantId: string;
  roomId: string;
  note?: string;
  rejectionReason?: string;
  reservationDate: Date;
  reservationHour: number;
  reservationMinute:number; 
  status?: string;
  createdAt?: Date;
  updatedAt?: Date;
  room?: Room;
  tenant?: User;
};

export const StatusReservation: { [key: string]: string } = {
  Pending: 'Đang chờ xử lý',
  Confirmed: 'Xác nhận',
  Rejected: 'Từ chối',
  Canceled: 'Đã hủy',
};
