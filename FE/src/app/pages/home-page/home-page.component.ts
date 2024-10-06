import { Component } from '@angular/core';
import { HomeFeedbackComponent } from '@features/home/components/home-feedback/home-feedback.component';
import { HomeSlideComponent } from '@features/home/components/home-slide/home-slide.component';
import { HomeStatsComponent } from '@features/home/components/home-stats/home-stats.component';
import { LessorListComponent } from '@features/lessor/components/lessor-list/lessor-list.component';
import { PostListComponent } from '@features/post/components/post-list/post-list.component';
import { ServiceListComponent } from '@features/service/components/service-list/service-list.component';

@Component({
  selector: 'app-home-page',
  standalone: true,
  imports: [
    ServiceListComponent,
    LessorListComponent,
    PostListComponent,
    HomeFeedbackComponent,
    HomeSlideComponent,
    HomeStatsComponent,
  ],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.scss',
})
export class HomePageComponent {
  posts: any;
}
