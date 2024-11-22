import { Component, computed, Input, signal } from '@angular/core';
import { PostItemComponent } from '../post-item/post-item.component';
import { Post } from '@features/post/models/post.model';

@Component({
  selector: 'app-post-list',
  standalone: true,
  imports: [PostItemComponent],
  templateUrl: './post-list.component.html',
  styleUrl: './post-list.component.scss',
})
export class PostListComponent {
  @Input()
  posts?: Post[];
  size = signal<number>(3);
  data = computed(() => this.posts?.slice(0, this.size()));

  viewMore() {
    this.size.update((v) => v + 3);
  }
}
