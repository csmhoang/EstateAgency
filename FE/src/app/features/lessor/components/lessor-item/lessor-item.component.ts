import { Component, DestroyRef, inject, Input } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { RouterLink } from '@angular/router';
import { User } from '@core/models/user.model';
import { UserService } from '@core/services/user.service';
import { LikeComponent } from '@shared/components/like/like.component';
import { catchError, of } from 'rxjs';

@Component({
  selector: 'app-lessor-item',
  standalone: true,
  imports: [LikeComponent, RouterLink],
  templateUrl: './lessor-item.component.html',
  styleUrl: './lessor-item.component.scss',
})
export class LessorItemComponent {
  @Input()
  lessor?: User;
  user = this.userService.currentUser();
  destroyRef = inject(DestroyRef);

  constructor(private userService: UserService) {}

  checkSave(lessorId?: string) {
    if (this.user && lessorId) {
      const followeeIds = this.user.followees?.map(
        (followee) => followee.followeeId
      );
      if (followeeIds) {
        return followeeIds.includes(lessorId);
      }
    }
    return false;
  }

  onSave(isFollow: boolean) {
    if (this.user && this.lessor) {
      this.userService
        .follow(this.user.id, this.lessor.id, isFollow)
        .pipe(
          takeUntilDestroyed(this.destroyRef),
          catchError(() => of(null))
        )
        .subscribe();
    }
  }
}
