import { User } from '@core/models/user.model';
import { CartDetail } from './cart-detail.model';

export type Cart = {
  id: string;
  tenantId: string;
  createdAt?: Date;
  updatedAt?: Date;

  tenant?: User;
  cartDetails?: CartDetail[];
};
