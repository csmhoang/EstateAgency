import { Room } from '@features/apartment/models/room.model';

export type LeaseDetail = {
  id: string;
  roomId: string;
  leaseId: string;
  startDate: Date;
  endDate: Date;
  numberOfTenant: number;
  createdAt?: Date;
  updatedAt?: Date;

  price: number;
  room?: Room;
};
