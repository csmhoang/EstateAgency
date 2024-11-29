import { Component } from '@angular/core';
import { RouterLink, RouterModule } from '@angular/router';
import { UserService } from '@core/services/user.service';
import { CartListComponent } from '@features/Cart/components/cart-list/cart-list.component';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CartListComponent, RouterModule],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.scss',
})
export class CartComponent {
  user = this.userService.currentUser;
  
  constructor(private userService: UserService) {}
}
