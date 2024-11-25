import {
  Component,
  computed,
  DestroyRef,
  inject,
  Input,
  OnInit,
} from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ReactiveFormsModule, FormControl } from '@angular/forms';
import { User } from '@core/models/user.model';
import { PresenceService } from '@core/services/presence.service';
import { UserService } from '@core/services/user.service';
import { ToastService } from '@shared/services/toast/toast.service';
import { catchError, of } from 'rxjs';

@Component({
  selector: 'app-lessor-detail-profile',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './lessor-detail-profile.component.html',
  styleUrl: './lessor-detail-profile.component.scss',
})
export class LessorDetailProfileComponent implements OnInit {
  @Input()
  lessor?: User;
  destroyRef = inject(DestroyRef);
  user = this.userService.currentUser();
  isFollow = new FormControl(false);
  isAuthentication = this.userService.isAuthenticated();
  isOnline = computed(() =>
    this.presenceService.onlineUsers().includes(this.lessor?.username!)
  );
  constructor(
    private presenceService: PresenceService,
    private userService: UserService,
    private toastService: ToastService
  ) {}

  ngOnInit() {
    if (this.user && this.lessor) {
      const followeeIds = this.user.followees?.map(
        (followee) => followee.followeeId
      );
      if (followeeIds) {
        const isFollow = followeeIds.includes(this.lessor.id);
        this.isFollow.setValue(!!isFollow, { emitEvent: false });
      }
      this.isFollow.valueChanges.subscribe((isFollow) => {
        if (isFollow !== undefined && isFollow !== null) {
          this.userService
            .follow(this.user?.id!, this.lessor?.id!, isFollow)
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

  timeSinceParticipateFilter(time: Date) {
    const now = new Date();
    const seconds = Math.floor(
      (now.getTime() - new Date(time).getTime()) / 1000
    );

    const intervals: { [key: string]: number } = {
      năm: 31536000,
      tháng: 2592000,
      tuần: 604800,
      ngày: 86400,
      giờ: 3600,
      phút: 60,
      giây: 1,
    };

    for (const [unit, value] of Object.entries(intervals)) {
      const interval = Math.floor(seconds / value);
      if (interval >= 1) {
        return `${interval} ${unit} trước`;
      }
    }

    return 'Vừa xong';
  }
}
