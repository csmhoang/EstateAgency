import { Component } from '@angular/core';
import { FeedbackSwiperComponent } from '@features/feedback-swiper/feedback-swiper.component';
import { FooterComponent } from '@features/footer/footer.component';
import { HeaderComponent } from '@features/header/header.component';
import { PostRecentComponent } from '@features/post/components/post-recent/post-recent.component';
import { RenterListComponent } from '@features/renter/components/renter-list/renter-list.component';
import { ServiceListComponent } from '@features/service/components/service-list/service-list.component';
import { SliderComponent } from '@features/slider/slider.component';
import { ScrollTopComponent } from '@shared/components/scroll-top/scroll-top.component';

@Component({
  selector: 'app-home-page',
  standalone: true,
  imports: [
    HeaderComponent,
    SliderComponent,
    ServiceListComponent,
    RenterListComponent,
    FeedbackSwiperComponent,
    PostRecentComponent,
    FooterComponent,
    ScrollTopComponent,
  ],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.scss',
})
export class HomePageComponent {
  posts: any;
}
