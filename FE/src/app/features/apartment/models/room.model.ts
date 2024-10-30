import { Photo } from './photo.model';

export type Room = {
  id: string;
  roomCode: number;
  landlordId: string;
  name: string;
  category: string;
  address: string;
  province: string;
  district: string;
  ward: string;
  bedroom: number;
  bathroom: number;
  toilet: number;
  interior: string;
  area: number;
  price: number;
  condition: string;
  photos?: Photo[];
};

export const Condition: { [key: string]: string } = {
  Available: 'Trống',
  Occupied: 'Đã thuê',
};

export const Category: { [key: string]: string } = {
  RentalRoom: 'Phòng trọ',
  MiniApartment: 'Chung cư mini',
  Apartment: 'Chung cư',
};

export const Interior: { [key: string]: string } = {
  Empty: 'Không',
  Full: 'Đầy đủ',
};
