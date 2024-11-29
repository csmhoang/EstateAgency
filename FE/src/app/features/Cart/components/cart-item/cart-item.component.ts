import { Component, Input } from '@angular/core';
import { CartDetail } from '@features/Cart/models/cart-detail.model';
import { NgbCarouselModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-cart-item',
  standalone: true,
  imports: [NgbCarouselModule],
  templateUrl: './cart-item.component.html',
  styleUrl: './cart-item.component.scss'
})
export class CartItemComponent {
  @Input() cartDetail?: CartDetail;
}
