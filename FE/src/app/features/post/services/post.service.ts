import { Injectable } from '@angular/core';
import { Post } from '../models/post.model';
import { Result } from '@core/models/result.model';
import { HttpClient, HttpContext, HttpParams } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { SpecParams } from '@core/models/spec-params.model';
import { PageData } from '@core/models/page-data.model';
import { SkipPreloader } from '@core/interceptors/skip.resolver';
import { RoomService } from '@features/apartment/services/room.service';

@Injectable({
  providedIn: 'root',
})
export class PostService {
  constructor(private http: HttpClient, private roomService: RoomService) {}

  getList(
    specParams: SpecParams,
    isHideLoading: boolean = false
  ): Observable<PageData<Post[]>> {
    let params = new HttpParams();
    Object.entries(specParams).forEach(([key, value]) => {
      if (value !== undefined && value !== null && value !== '') {
        params = params.set(key, value.toString());
      }
    });

    return this.http
      .get<Result<PageData<Post[]>>>('/posts/list', {
        params,
        context: new HttpContext().set(SkipPreloader, isHideLoading),
      })
      .pipe(map((response) => response.data));
  }

  getDetail(id: string) {
    return this.http
      .get<Result<Post>>(`/posts/detail/${id}`)
      .pipe(map((response) => response.data));
  }

  getRooms() {
    return this.roomService.get(true);
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
