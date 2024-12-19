import { CommonModule } from '@angular/common';
import {
  Component,
  computed,
  DestroyRef,
  inject,
  Input,
  OnInit,
} from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { User } from '@core/models/user.model';
import { PresenceService } from '@core/services/presence.service';
import { UserService } from '@core/services/user.service';
import { CartAppendComponent } from '@features/Cart/components/cart-append/cart-append.component';
import { CartService } from '@features/Cart/services/cart.service';
import { MessengerComponent } from '@features/messenger/components/messenger/messenger.component';
import { Post } from '@features/post/models/post.model';
import { ReservationInsertComponent } from '@features/reservation/components/reservation-insert/reservation-insert.component';
import { DialogService } from '@shared/services/dialog/dialog.service';
import { ToastService } from '@shared/services/toast/toast.service';
import { catchError, debounceTime, of } from 'rxjs';

@Component({
  selector: 'app-lessor-info-card',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './lessor-info-card.component.html',
  styleUrl: './lessor-info-card.component.scss',
})
export class LessorInfoCardComponent implements OnInit {
  @Input() post?: Post;
  landlord?: User;
  destroyRef = inject(DestroyRef);
  user = this.userService.currentUser;
  cart = this.cartService.currentCart;

  isFollow = new FormControl(false);
  isAuthentication = this.userService.isAuthenticated();

  isOnline = computed(() => {
    if (this.landlord) {
      return this.presenceService.onlineUsers().includes(this.landlord.id);
    }
    return false;
  });

  constructor(
    private dialogService: DialogService,
    private presenceService: PresenceService,
    private userService: UserService,
    private toastService: ToastService,
    private cartService: CartService
  ) {}

  ngOnInit() {
    this.landlord = this.post?.landlord;
    if (this.user() && this.landlord) {
      const followeeIds = this.user()?.followees?.map(
        (followee) => followee.followeeId
      );
      if (followeeIds) {
        const isFollow = followeeIds.includes(this.landlord.id);
        this.isFollow.setValue(!!isFollow, { emitEvent: false });
      }
      this.isFollow.valueChanges.subscribe((isFollow) => {
        if (isFollow !== undefined && isFollow !== null) {
          this.userService
            .follow(this.user()?.id!, this.landlord?.id!, isFollow)
            .pipe(
              debounceTime(1000),
              takeUntilDestroyed(this.destroyRef),
              catchError(() => of(null))
            )
            .subscribe((response) => {
              if (response?.success) {
                this.userService
                  .init(true)
                  .pipe(
                    takeUntilDestroyed(this.destroyRef),
                    catchError(() => of(null))
                  )
                  .subscribe();
                if (isFollow) {
                  this.landlord!.numberOfFollowers!++;
                  this.toastService.success('Đã theo dõi');
                } else {
                  this.landlord!.numberOfFollowers!--;
                  this.toastService.success('Đã hủy theo dõi');
                }
              }
            });
        }
      });
    }
  }

  onReservation() {
    if (this.post && this.user()) {
      const isCheckExist = this.user()?.reservations?.some(
        (reservation) =>
          reservation.roomId === this.post?.room?.id &&
          (reservation.status === 'Pending' ||
            reservation.status === 'Confirmed')
      );
      if (!isCheckExist) {
        if (!this.post?.room?.visibility) {
          return this.toastService.warn('Phòng đã bị xóa.');
        }
        if (this.post?.room?.condition === 'Occupied') {
          return this.toastService.warn('Phòng không có sẵn.');
        }

        this.dialogService
          .form(ReservationInsertComponent, this.post, 'lg')
          .then(() => {
            this.toastService.success(
              'Yêu cầu đặt lịch đã gửi, xin hãy chờ chủ nhà xác nhận.'
            );
            this.userService
              .init(true)
              .pipe(
                takeUntilDestroyed(this.destroyRef),
                catchError(() => of(null))
              )
              .subscribe();
          });
      } else {
        this.toastService.warn('Yêu cầu đặt lịch của bạn đang được xử lý.');
      }
    }
  }

  onAppendCart() {
    if (this.post && this.cart()) {
      const isCheckExist = this.cart()?.cartDetails?.some(
        (cartDetail) => cartDetail.room?.id === this.post?.roomId
      );
      if (!isCheckExist) {
        if (!this.post?.room?.visibility) {
          return this.toastService.warn('Phòng đã bị xóa.');
        }
        if (this.post?.room?.condition === 'Occupied') {
          return this.toastService.warn('Phòng đã có người thuê.');
        }

        this.dialogService
          .form(CartAppendComponent, this.post, 'lg')
          .then(() => {
            this.toastService.success('Đã thêm phòng vào giỏ.');
            this.cartService
              .init(true)
              .pipe(
                takeUntilDestroyed(this.destroyRef),
                catchError(() => of(null))
              )
              .subscribe();
          });
      } else {
        this.toastService.warn('Phòng đã có trong giỏ');
      }
    }
  }

  onChat(otherId?: string) {
    this.dialogService.form(MessengerComponent, otherId);
  }
}
