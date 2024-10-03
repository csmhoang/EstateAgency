import { Component, Input } from '@angular/core';
import { NgbCarouselModule } from '@ng-bootstrap/ng-bootstrap';
import { LikeComponent } from '@shared/components/like/like.component';

@Component({
  selector: 'app-post-item',
  standalone: true,
  imports: [NgbCarouselModule, LikeComponent],
  templateUrl: './post-item.component.html',
  styleUrl: './post-item.component.scss',
})
export class PostItemComponent {
  @Input()
  item: any;

  images = [
    {
      url: '/assets/img/properties/property-1.jpg',
      title: 'Bedroom',
      description: 'Spacious bedroom with king-size bed',
    },
    {
      url: '/assets/img/properties/property-2.jpg',
      title: 'Bathroom',
      description: 'Modern bathroom with rainfall shower',
    },
    {
      url: '/assets/img/properties/property-3.jpg',
      title: 'Living Area',
      description: 'Comfortable living space with city view',
    },
    {
      url: '/assets/img/properties/property-4.jpg',
      title: 'Kitchenette',
      description: 'Fully equipped kitchenette',
    },
    {
      url: '/assets/img/properties/property-5.jpg',
      title: 'Work Desk',
      description: 'Ergonomic work area',
    },
    {
      url: '/assets/img/properties/property-6.jpg',
      title: 'Balcony',
      description: 'Private balcony with seating',
    },
  ];
}
