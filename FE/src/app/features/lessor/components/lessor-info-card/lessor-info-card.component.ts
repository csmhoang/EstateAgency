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
import { BookingInsertComponent } from '@features/booking/components/booking-insert/booking-insert.component';
import { Post } from '@features/post/models/post.model';
import { ReservationInsertComponent } from '@features/reservation/components/reservation-insert/reservation-insert.component';
import { ReservationService } from '@features/reservation/services/reservation.service';
import { DialogService } from '@shared/services/dialog/dialog.service';
import { ToastService } from '@shared/services/toast/toast.service';
import { catchError, firstValueFrom, of } from 'rxjs';

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
  user = this.userService.currentUser();
  isFollow = new FormControl(false);
  isAuthentication = this.userService.isAuthenticated();
  isReservation = false;
  isBooking = false;

  isOnline = computed(() =>
    this.presenceService.onlineUsers().includes(this.landlord?.username!)
  );

  constructor(
    private dialogService: DialogService,
    private presenceService: PresenceService,
    private userService: UserService,
    private toastService: ToastService,
    private reservationService: ReservationService
  ) {}

  ngOnInit() {
    this.landlord = this.post?.landlord;
    if (this.user && this.landlord) {
      const followeeIds = this.user.followees?.map(
        (followee) => followee.followeeId
      );
      if (followeeIds) {
        const isFollow = followeeIds.includes(this.landlord.id);
        this.isFollow.setValue(!!isFollow, { emitEvent: false });
      }
      this.isFollow.valueChanges.subscribe((isFollow) => {
        if (isFollow !== undefined && isFollow !== null) {
          this.userService
            .follow(this.user?.id!, this.landlord?.id!, isFollow)
            .pipe(
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
                  this.toastService.success('Đã theo dõi');
                } else {
                  this.toastService.success('Đã hủy theo dõi');
                }
              }
            });
        }
      });
    }
  }

  onReservation() {
    if (this.post && this.user) {
      const isCheckExist = this.user.reservations?.some(
        (reservation) =>
          reservation.postId === this.post?.id &&
          (reservation.status === 'Pending' ||
            reservation.status === 'Confirmed')
      );
      if (!isCheckExist && !this.isReservation) {
        this.dialogService
          .form(ReservationInsertComponent, this.post, 'lg')
          .then(() => {
            this.toastService.success(
              'Yêu cầu đặt lịch đã gửi, xin hãy chờ chủ nhà xác nhận.'
            );
            this.isReservation = true;
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
  onBooking() {
    if (this.post && this.user) {
      const isCheckExist = this.user.bookings?.some(
        (booking) =>
          booking.postId === this.post?.id &&
          (booking.status === 'Pending' || booking.status === 'Accepted')
      );
      if (!isCheckExist && !this.isBooking) {
        this.dialogService
          .form(BookingInsertComponent, this.post, 'lg')
          .then(() => {
            this.toastService.success(
              'Yêu cầu đặt phòng đã gửi, xin hãy chờ chủ nhà xác nhận.'
            );
            this.isBooking = true;
            this.userService
              .init(true)
              .pipe(
                takeUntilDestroyed(this.destroyRef),
                catchError(() => of(null))
              )
              .subscribe();
          });
      } else {
        this.toastService.warn('Yêu cầu đặt phòng của bạn đang được xử lý.');
      }
    }
  }
}
