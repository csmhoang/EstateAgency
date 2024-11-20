import { CommonModule } from '@angular/common';
import { Component, DestroyRef, inject, Input } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { UserService } from '@core/services/user.service';
import {
  Category,
  Interior,
  Price,
} from '@features/apartment/models/room.model';
import { Post } from '@features/post/models/post.model';
import { SavePost } from '@features/post/models/save-post.model';
import { PostService } from '@features/post/services/post.service';
import { LikeComponent } from '@shared/components/like/like.component';
import { catchError, of } from 'rxjs';

@Component({
  selector: 'app-apartment-primary-info',
  standalone: true,
  imports: [LikeComponent, CommonModule],
  templateUrl: './apartment-primary-info.component.html',
  styleUrl: './apartment-primary-info.component.scss',
})
export class ApartmentPrimaryInfoComponent {
  @Input() post?: Post;
  destroyRef = inject(DestroyRef);
  isAuthentication = this.userService.isAuthenticated();
  user = this.userService.currentUser();

  priceFilter = Price;
  categoryFilter = Category;
  interiorFilter = Interior;

  constructor(
    private userService: UserService,
    private postService: PostService
  ) {}

  checkSave(postId?: string) {
    if (this.user && postId) {
      const savePosts = this.user.savePosts?.map((savePost) => savePost.postId);
      return savePosts?.includes(postId);
    }
    return false;
  }

  onSave(isSave: boolean) {
    if (this.user && this.post) {
      const savePost: SavePost = {
        userId: this.user.id,
        postId: this.post.id,
      };
      this.postService
        .savePost(savePost, isSave)
        .pipe(
          takeUntilDestroyed(this.destroyRef),
          catchError(() => of(null))
        )
        .subscribe();
    }
  }

  timeSinceUpdateFilter(time: Date) {
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
