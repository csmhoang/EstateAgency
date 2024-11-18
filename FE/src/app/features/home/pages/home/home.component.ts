import { CommonModule } from '@angular/common';
import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { IfAuthenticatedDirective } from '@core/auth/directives/if-authenticated.directive';
import { FooterComponent } from '@core/layout/footer/footer.component';
import { HeaderComponent } from '@core/layout/header/header.component';
import { User } from '@core/models/user.model';
import { UserService } from '@core/services/user.service';
import { HomeFeedbackComponent } from '@features/home/components/home-feedback/home-feedback.component';
import { HomeSlideComponent } from '@features/home/components/home-slide/home-slide.component';
import { HomeStatsComponent } from '@features/home/components/home-stats/home-stats.component';
import { LessorListComponent } from '@features/lessor/components/lessor-list/lessor-list.component';
import { PostListComponent } from '@features/post/components/post-list/post-list.component';
import { Post } from '@features/post/models/post.model';
import { PostService } from '@features/post/services/post.service';
import { ServiceListComponent } from '@features/service/components/service-list/service-list.component';
import { firstValueFrom, catchError, of } from 'rxjs';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    HeaderComponent,
    FooterComponent,
    ServiceListComponent,
    LessorListComponent,
    PostListComponent,
    HomeFeedbackComponent,
    HomeSlideComponent,
    HomeStatsComponent,
    CommonModule,
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent implements OnInit {
  destroyRef = inject(DestroyRef);
  posts?: Post[];
  lessors?: User[];

  constructor(
    private postService: PostService,
    private userService: UserService
  ) {}

  async ngOnInit() {
    this.posts = await firstValueFrom(
      this.postService.getListRecent().pipe(
        takeUntilDestroyed(this.destroyRef),
        catchError(() => of([]))
      )
    );

    this.lessors = await firstValueFrom(
      this.userService.getFamous().pipe(
        takeUntilDestroyed(this.destroyRef),
        catchError(() => of([]))
      )
    );
  }
}
