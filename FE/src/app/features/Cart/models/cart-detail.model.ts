import { Room } from "@features/apartment/models/room.model";
import { Cart } from "./cart.model";

export type CartDetail = {
  id: string;
  cartId: string;
  roomId: string;
  numberOfMonth: number;
  numberOfTenant: number;
  price: number;
  createdAt?: Date;
  updatedAt?: Date;
  cart?: Cart;
  room?: Room;
};
