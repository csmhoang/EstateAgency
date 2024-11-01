import { Injectable } from '@angular/core';
import { Post } from '../models/post.model';
import { Result } from '@core/models/result.model';
import { HttpClient, HttpContext, HttpParams } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { SpecParams } from '@core/models/spec-params.model';
import { PageData } from '@core/models/page-data.model';
import { SkipPreloader } from '@core/interceptors/skip.resolver';
import { ApartmentService } from '@features/apartment/services/apartment.service';

@Injectable({
  providedIn: 'root',
})
export class PostService {
  constructor(
    private http: HttpClient,
    private apartmentService: ApartmentService
  ) {}

  getList(
    specParams: SpecParams,
    isHideLoading: boolean = false
  ): Observable<PageData<Post[]>> {
    let params = new HttpParams();
    Object.entries(specParams).forEach(([key, value]) => {
      if (value) {
        params = params.set(key, value);
      }
    });

    return this.http
      .get<Result<PageData<Post[]>>>('/posts/list', {
        params,
        context: new HttpContext().set(SkipPreloader, isHideLoading),
      })
      .pipe(map((response) => response.data));
  }

  getRooms() {
    return this.apartmentService.get(true);
  }

  insert(post: Post) {
    return this.http.post<Result>('/posts', post);
  }

  update(id: string, post: Post) {
    return this.http.put<Result>(`/posts?id=${id}`, post);
  }

  remove(postId: string) {
    return this.http.delete<Result>(`/posts/remove?id=${postId}`);
  }
}
