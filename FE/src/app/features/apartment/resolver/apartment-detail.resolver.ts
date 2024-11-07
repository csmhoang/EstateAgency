import { DestroyRef, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ResolveFn } from '@angular/router';
import { Post } from '@features/post/models/post.model';
import { PostService } from '@features/post/services/post.service';
import { firstValueFrom, catchError, of } from 'rxjs';

export const apartmentDetailResolver: ResolveFn<Post | null> = (
  route,
  state
) => {
  const destroyRef = inject(DestroyRef);
  const postId = route.paramMap.get('id');
  if (!postId) {
    return null;
  }
  const postService = inject(PostService);
  return firstValueFrom(
    postService.getDetail(postId).pipe(
      takeUntilDestroyed(destroyRef),
      catchError(() => of(null))
    )
  );
};
