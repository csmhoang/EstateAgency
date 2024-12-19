import { HttpClient, HttpContext, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SkipPreloader } from '@core/interceptors/skip.resolver';
import { Result } from '@core/models/result.model';

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  constructor(private http: HttpClient) {}
  response(id: string, status: string) {
    let params = new HttpParams().set('id', id).set('status', status);

    return this.http.put<Result>('/notifications/response', null, {
      params,
      context: new HttpContext().set(SkipPreloader, true),
    });
  }
}
