import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CartDetail } from '../models/cart-detail.model';
import { Result } from '@core/models/result.model';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  constructor(private http: HttpClient) {}

  getAll() {
    return this.http.get<Result<CartDetail>>('/carts/detail');
  }

  append(cartDetail: CartDetail) {
    return this.http.post<Result>('/carts/append', cartDetail);
  }

  remove(id: string) {
    return this.http.delete<Result>(`/carts/remove?id=${id}`);
  }
}
