import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatBadgeModule } from '@angular/material/badge';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { RouterModule } from '@angular/router';
import { IfCustomerDirective } from '@core/auth/directives/if-customer.directive';
import { AuthService } from '@core/auth/services/auth.service';
import { UserService } from '@core/services/user.service';
import { CartService } from '@features/Cart/services/cart.service';

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [
    IfCustomerDirective,
    RouterModule,
    CommonModule,
    MatButtonModule,
    MatMenuModule,
    MatBadgeModule,
  ],
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.scss',
})
export class MenuComponent {
  cart = this.cartService.currentCart;
  user = this.userService.currentUser;

  constructor(
    private authService: AuthService,
    private cartService: CartService,
    private userService: UserService,
  ) {}

  onLogout() {
    this.authService.logout();
  }
}
