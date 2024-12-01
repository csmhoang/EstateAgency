import { CommonModule } from '@angular/common';
import { Component, DestroyRef, inject, OnDestroy } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { RouterModule } from '@angular/router';
import { FooterComponent } from '@core/layout/footer/footer.component';
import { HeaderComponent } from '@core/layout/header/header.component';
import { BookingService } from '@features/booking/services/booking.service';
import { CartListComponent } from '@features/Cart/components/cart-list/cart-list.component';
import { CartService } from '@features/Cart/services/cart.service';
import { ToastService } from '@shared/services/toast/toast.service';
import { catchError, of } from 'rxjs';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [
    CartListComponent,
    RouterModule,
    FooterComponent,
    HeaderComponent,
    CommonModule,
  ],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.scss',
})
export class CartComponent implements OnDestroy {
  destroyRef = inject(DestroyRef);

  cart = this.cartService.currentCart;
  constructor(
    private cartService: CartService,
    private bookingService: BookingService,
    private toastService: ToastService
  ) {}

  onBooking() {
    this.bookingService
      .insert()
      .pipe(
        takeUntilDestroyed(this.destroyRef),
        catchError(() => of(null))
      )
      .subscribe((response) => {
        if (response?.success) {
          this.cart.set(null);
          this.toastService.success(
            'Yêu cầu đặt phòng đã gửi, xin hãy chờ chủ nhà xác nhận.'
          );
        }
      });
  }

  ngOnDestroy() {
    const cart = this.cart();
    if (cart && this.cartService.isCartChanges()) {
      this.cartService
        .update(cart.id, cart, true)
        .pipe(catchError(() => of(null)))
        .subscribe(() => {
          this.cartService.isCartChanges.set(false);
        });
    }
  }
}
