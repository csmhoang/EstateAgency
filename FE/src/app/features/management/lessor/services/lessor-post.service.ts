import { Injectable, signal } from '@angular/core';
import { PageData } from '@core/models/page-data.model';
import { SpecParams } from '@core/models/spec-params.model';
import { Post } from '@features/post/models/post.model';
import { PostService } from '@features/post/services/post.service';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LessorPostService {
  pageSignal = signal<PageData<Post[]> | null>(null);
  public page = this.pageSignal.asReadonly();
  public specParams = signal<SpecParams>({
    pageSize: 5,
    pageIndex: 1,
  });

  constructor(private postService: PostService) {}

  loadData(isHideLoading: boolean = false) {
    return this.postService.getList(this.specParams(), isHideLoading).pipe(
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