import { Component, Input } from '@angular/core';
import { PostRootItemComponent } from '../post-root-item/post-root-item.component';
import { Post } from '@features/post/models/post.model';

@Component({
  selector: 'app-post-root-list',
  standalone: true,
  imports: [PostRootItemComponent],
  templateUrl: './post-root-list.component.html',
  styleUrl: './post-root-list.component.scss',
})
export class PostRootListComponent {
  @Input()
  posts!: Post[];
}
