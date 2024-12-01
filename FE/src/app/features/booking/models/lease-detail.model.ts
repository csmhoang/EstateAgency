import { Room } from '@features/apartment/models/room.model';

export type LeaseDetail = {
  id: string;
  roomId: string;
  leaseId: string;
  numberOfMonth: number;
  numberOfTenant: number;
  price: number;
  room?: Room;
};
