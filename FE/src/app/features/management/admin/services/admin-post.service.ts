import { Injectable, signal } from '@angular/core';
import { PageData } from '@core/models/page-data.model';
import { Post } from '@features/post/models/post.model';
import { SpecPostParams } from '@features/post/models/spec-post-params.model';
import { PostService } from '@features/post/services/post.service';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AdminPostService {
  pageSignal = signal<PageData<Post[]> | null>(null);
  public page = this.pageSignal.asReadonly();
  public specParams = signal<SpecPostParams>({});

  constructor(private postService: PostService) {}

  loadData(isDisplayMiniLoading: boolean = true) {
    return this.postService
      .getList(this.specParams(), isDisplayMiniLoading)
      .pipe(
        tap({
          next: (page) => {
            if (page) {
              this.pageSignal.set(page);
            }
          },
        })
      );
  }

  remove(postId: string) {
    return this.postService.remove(postId);
  }
}
