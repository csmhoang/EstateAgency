import { Injectable } from '@angular/core';
import { Post } from '../models/post.model';
import { Result } from '@core/models/result.model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PostService {
  constructor(private http: HttpClient) {}

  insert(post: Post): Observable<Result> {
    return this.http.post<Result>('/rooms', post);
  }
}
