import { HttpClient, HttpContext } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { CartDetail } from '../models/cart-detail.model';
import { Result } from '@core/models/result.model';
import { Cart } from '../models/cart.model';
import { Observable, shareReplay, tap } from 'rxjs';
import { SkipPreloader } from '@core/interceptors/skip.resolver';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private currentCartSignal = signal<Cart | null>(null);
  public currentCart = this.currentCartSignal;
  public isCartChanges = signal<boolean>(false);

  constructor(private http: HttpClient) {}

  init(isHideLoading: boolean = false): Observable<Result<Cart>> {
    return this.http
      .get<Result<Cart>>('/carts/current', {
        context: new HttpContext().set(SkipPreloader, isHideLoading),
      })
      .pipe(
        tap({
          next: (res) => {
            this.currentCartSignal.set(res.data);
          },
          error: () => this.currentCartSignal.set(null),
        }),
        shareReplay(1)
      );
  }

  getAll() {
    return this.http.get<Result<CartDetail>>('/carts/detail');
  }

  append(cartDetail: CartDetail) {
    return this.http.post<Result>('/carts/append', cartDetail);
  }

  remove(id: string) {
    return this.http.delete<Result>(`/carts/remove?cartDetailId=${id}`);
  }

  update(id: string, cart: Cart, isHideLoading: boolean = false) {
    return this.http.put<Result>(`/carts?cartId=${id}`, cart, {
      context: new HttpContext().set(SkipPreloader, isHideLoading),
    });
  }

  purCart() {
    this.currentCartSignal.set(null);
  }
}
