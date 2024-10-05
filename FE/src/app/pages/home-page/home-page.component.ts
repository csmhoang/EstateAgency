import { Component } from '@angular/core';
import { FooterComponent } from '@features/footer/footer.component';
import { HeaderComponent } from '@features/header/header.component';
import { HomeFeedbackComponent } from '@features/home/components/home-feedback/home-feedback.component';
import { HomeSlideComponent } from '@features/home/components/home-slide/home-slide.component';
import { HomeStatsComponent } from '@features/home/components/home-stats/home-stats.component';
import { LessorListComponent } from '@features/lessor/components/lessor-list/lessor-list.component';
import { PostListComponent } from '@features/post/components/post-list/post-list.component';
import { ServiceListComponent } from '@features/service/components/service-list/service-list.component';
import { ScrollTopComponent } from '@shared/components/scroll-top/scroll-top.component';

@Component({
  selector: 'app-home-page',
  standalone: true,
  imports: [
    HeaderComponent,
    ServiceListComponent,
    LessorListComponent,
    PostListComponent,
    FooterComponent,
    ScrollTopComponent,
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
