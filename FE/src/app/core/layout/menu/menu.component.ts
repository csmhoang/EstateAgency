import { CommonModule } from '@angular/common';
import {
  Component,
  computed,
  DestroyRef,
  inject,
  ViewEncapsulation,
} from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatBadgeModule } from '@angular/material/badge';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { RouterModule } from '@angular/router';
import { IfCustomerDirective } from '@core/auth/directives/if-customer.directive';
import { AuthService } from '@core/auth/services/auth.service';
import { PresenceService } from '@core/services/presence.service';
import { UserService } from '@core/services/user.service';
import { CartService } from '@features/Cart/services/cart.service';
import { MessengerComponent } from '@features/messenger/components/messenger/messenger.component';
import { NotificationBoxComponent } from '@features/notification/pages/notification-box/notification-box.component';
import { NotificationService } from '@features/notification/services/notification.service';
import { DialogService } from '@shared/services/dialog/dialog.service';
import { catchError, of } from 'rxjs';

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
    NotificationBoxComponent,
  ],
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.scss',
  encapsulation: ViewEncapsulation.None,
})
export class MenuComponent {
  destroyRef = inject(DestroyRef);
  cart = this.cartService.currentCart;
  user = this.userService.currentUser;
  role = computed(() => {
    if (this.userService.isLandlord()) {
      return 'lessor';
    } else if (this.userService.isAdmin()) {
      return 'admin';
    }
    return '';
  });
  notifications = this.presenceService.notificationThread;
  unreadNotifications = computed(() => {
    return this.notifications().filter(
      (notification) => notification.status === 'Unread'
    );
  });

  constructor(
    private authService: AuthService,
    private cartService: CartService,
    private userService: UserService,
    private dialogService: DialogService,
    private presenceService: PresenceService,
    private notificationService: NotificationService
  ) {}

  onLogout() {
    this.authService.logout();
  }

  onChat() {
    if (this.user()) {
      this.dialogService.form(MessengerComponent, null);
    }
  }

  onNotification() {
    if (this.unreadNotifications()) {
      this.notifications.update((notifications) =>
        notifications.map((notification) => {
          if (notification.status === 'Unread') {
            notification.status = 'Readed';
            this.notificationService
              .response(notification.id!, 'Readed')
              .pipe(
                takeUntilDestroyed(this.destroyRef),
                catchError(() => of(null))
              )
              .subscribe();
          }
          return notification;
        })
      );
    }
  }
}
