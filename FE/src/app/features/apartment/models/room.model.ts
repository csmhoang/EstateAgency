import { User } from '@core/models/user.model';
import { Photo } from './photo.model';
import { Post } from '@features/post/models/post.model';

export type Room = {
  id: string;
  landlordId: string;
  name: string;
  category: string;
  address: string;
  province: string;
  district?: string;
  ward?: string;
  bedroom: number;
  bathroom: number;
  toilet: number;
  interior: string;
  area: number;
  price: number;
  condition: string;
  landlord?: User;
  createdAt?: Date;
  updatedAt?: Date;
  photos?: Photo[];
  posts?: Post[];
};

export const ConditionRoom: { [key: string]: string } = {
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

export const Price = (value: number = 1) => {
  if (value >= 1_000_000) {
    const millions = (value / 1_000_000).toFixed(1);
    return `${millions} triệu/tháng`;
  } else if (value >= 1_000) {
    const hundred = (value / 1_000).toFixed(1);
    return `${hundred} trăm/tháng`;
  }
  return value.toString();
};
