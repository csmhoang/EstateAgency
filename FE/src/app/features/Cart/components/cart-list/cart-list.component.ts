import { Component, Input } from '@angular/core';
import { CartItemComponent } from '../cart-item/cart-item.component';
import { CartDetail } from '@features/Cart/models/cart-detail.model';

@Component({
  selector: 'app-cart-list',
  standalone: true,
  imports: [CartItemComponent],
  templateUrl: './cart-list.component.html',
  styleUrl: './cart-list.component.scss',
})
export class CartListComponent {
  @Input() cartDetails?: CartDetail[];

  sumPrice = this.cartDetails?.reduce(
    (accumulator, cartDetail) => accumulator + cartDetail.price,
    0
  );
}
