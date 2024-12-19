import { HttpClient, HttpContext, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Result } from '@core/models/result.model';
import { map } from 'rxjs';
import { Lease } from '../models/lease.model';
import { SkipPreloader } from '@core/interceptors/skip.resolver';

@Injectable({
  providedIn: 'root',
})
export class LeaseService {
  constructor(private http: HttpClient) {}

  getByRoomId(roomId: string, isDisplayMiniLoading: boolean = false) {
    return this.http
      .get<Result<Lease[]>>(`/leases/roomgId/${roomId}`, {
        context: new HttpContext().set(SkipPreloader, isDisplayMiniLoading),
      })
      .pipe(map((response) => response.data));
  }

  insert(lease: Lease) {
    return this.http.post<Result>('/leases', lease);
  }

  response(id: string, status: string) {
    let params = new HttpParams().set('id', id).set('status', status);

    return this.http.put<Result>('/leases/response', null, {
      params,
      context: new HttpContext().set(SkipPreloader, true),
    });
  }

}
