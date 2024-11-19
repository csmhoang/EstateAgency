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
import { Post } from '@features/post/models/post.model';
import { ReservationInsertComponent } from '@features/reservation/components/reservation-insert/reservation-insert.component';
import { DialogService } from '@shared/services/dialog/dialog.service';
import { ToastService } from '@shared/services/toast/toast.service';
import { catchError, of } from 'rxjs';

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

  isOnline = computed(() =>
    this.presenceService.onlineUsers().includes(this.landlord?.username!)
  );

  constructor(
    private dialogService: DialogService,
    private presenceService: PresenceService,
    private userService: UserService,
    private toastService: ToastService
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
              }
            });
        }
      });
    }
  }

  onReservation() {
    this.dialogService
      .form(ReservationInsertComponent, this.post, 'lg')
      .then(() => {
        this.toastService.success('Đã gửi yêu cầu đặt lịch');
      });
  }
}
