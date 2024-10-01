import { Component, Input } from '@angular/core';
import {
  NgbCarouselModule,
} from '@ng-bootstrap/ng-bootstrap';
import { HeartLikeComponent } from '../../shared/components/heart-like/heart-like.component';

@Component({
  selector: 'app-post',
  standalone: true,
  imports: [NgbCarouselModule, HeartLikeComponent],
  templateUrl: './post.component.html',
  styleUrl: './post.component.scss',
})
export class PostComponent {
  @Input()
  item: any;
  
  images = [
    {
      url: '/assets/images/room.jpg',
      title: 'Bedroom',
      description: 'Spacious bedroom with king-size bed',
    },
    {
      url: '/assets/images/room.jpg',
      title: 'Bathroom',
      description: 'Modern bathroom with rainfall shower',
    },
    {
      url: '/assets/images/room.jpg',
      title: 'Living Area',
      description: 'Comfortable living space with city view',
    },
    {
      url: '/assets/images/room.jpg',
      title: 'Kitchenette',
      description: 'Fully equipped kitchenette',
    },
    {
      url: '/assets/images/room.jpg',
      title: 'Work Desk',
      description: 'Ergonomic work area',
    },
    {
      url: '/assets/images/room.jpg',
      title: 'Balcony',
      description: 'Private balcony with seating',
    },
  ];
}
