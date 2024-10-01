import { Component } from '@angular/core';
import { HeaderComponent } from '../header/header.component';
import { SliderComponent } from '../slider/slider.component';
import { ServiceListComponent } from '../service/components/service-list/service-list.component';
import { RenterListComponent } from '../renter/components/renter-list/renter-list.component';
import { SwiperComponent } from '../swiper/swiper.component';
import { PostRecentComponent } from '../post/components/post-recent/post-recent.component';
import { FooterComponent } from '../footer/footer.component';
import { ScrollTopComponent } from '../../shared/components/scroll-top/scroll-top.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    HeaderComponent,
    SliderComponent,
    ServiceListComponent,
    RenterListComponent,
    SwiperComponent,
    PostRecentComponent,
    FooterComponent,
    ScrollTopComponent,
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent {
  posts: any;
}
