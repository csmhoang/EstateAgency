import { Injectable, signal } from '@angular/core';
import { PageData } from '@core/models/page-data.model';
import { Post } from '@features/post/models/post.model';
import { tap } from 'rxjs';
import { PostService } from '@features/post/services/post.service';
import { SpecPostParams } from '@features/post/models/spec-post-params.model';

@Injectable({
  providedIn: 'root',
})
export class ApartmentService {
  pageSignal = signal<PageData<Post[]> | null>(null);
  public page = this.pageSignal.asReadonly();
  public specPostParams = signal<SpecPostParams>({});

  constructor(private postService: PostService) {}

  loadData(isDisplayMiniLoading: boolean = true) {
    return this.postService.getList(this.specPostParams(), isDisplayMiniLoading).pipe(
      tap({
        next: (page) => {
          if (page) {
            this.pageSignal.set(page);
          }
        },
      })
    );
  }
}
