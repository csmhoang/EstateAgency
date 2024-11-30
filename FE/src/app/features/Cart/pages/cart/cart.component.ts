import { CommonModule } from '@angular/common';
import { Component, DestroyRef, inject, OnDestroy } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { RouterModule } from '@angular/router';
import { FooterComponent } from '@core/layout/footer/footer.component';
import { HeaderComponent } from '@core/layout/header/header.component';
import { CartListComponent } from '@features/Cart/components/cart-list/cart-list.component';
import { CartService } from '@features/Cart/services/cart.service';
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
  cart = this.cartService.currentCart;
  constructor(private cartService: CartService) {}

  ngOnDestroy() {
    const cart = this.cart();
    if (cart && this.cartService.isCartChanges()) {
      this.cartService
        .update(cart.id, cart, true)
        .pipe(catchError(() => of(null)))
        .subscribe();
    }
  }
}
