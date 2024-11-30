import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { CartItemComponent } from '../cart-item/cart-item.component';
import { CartDetail } from '@features/Cart/models/cart-detail.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-cart-list',
  standalone: true,
  imports: [CartItemComponent, CommonModule],
  templateUrl: './cart-list.component.html',
  styleUrl: './cart-list.component.scss',
})
export class CartListComponent implements OnInit{
  @Input() cartDetails?: CartDetail[];

  sumPrice?:number;

  ngOnInit(): void {
    this.sumPrice = this.cartDetails?.reduce(
      (accumulator, cartDetail) => accumulator + cartDetail.price,
      0
    );
  }
}
