import { Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Price } from '@features/apartment/models/room.model';
import { Post } from '@features/post/models/post.model';
import { NgbCarouselModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-post-item',
  standalone: true,
  imports: [NgbCarouselModule, RouterLink],
  templateUrl: './post-item.component.html',
  styleUrl: './post-item.component.scss',
})
export class PostItemComponent {
  @Input()
  post!: Post;

  priceFilter = Price;
}
