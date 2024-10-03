import { Component } from '@angular/core';
import { FeedbackSwiperComponent } from '@features/feedback-swiper/feedback-swiper.component';
import { FooterComponent } from '@features/footer/footer.component';
import { HeaderComponent } from '@features/header/header.component';
import { CommentComponent } from '@shared/components/comment/comment.component';
import { ScrollTopComponent } from '@shared/components/scroll-top/scroll-top.component';

@Component({
  selector: 'app-apartment-detail-page',
  standalone: true,
  imports: [
    HeaderComponent,
    FeedbackSwiperComponent,
    FooterComponent,
    ScrollTopComponent,
    CommentComponent
  ],
  templateUrl: './apartment-detail-page.component.html',
  styleUrl: './apartment-detail-page.component.scss',
})
export class ApartmentDetailPageComponent {}
