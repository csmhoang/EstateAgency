import { HttpClient, HttpContext } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  SkipApi,
  SkipPreloader,
  SkipToken,
} from '@core/interceptors/skip.resolver';
import { Response } from '@core/models/response.model';
import { map, Observable } from 'rxjs';
import { Place } from '../models/place.model';
import { Room } from '../models/room.model';

@Injectable({
  providedIn: 'root',
})
export class PostService {
  url = 'https://esgoo.net/api-tinhthanh';

  constructor(private http: HttpClient) {}

  insert(room: Room, files: File[]): Observable<Response> {
    const form = new FormData();
    Object.entries(room).forEach(([key, value]) => {
      if (value instanceof Date) {
        form.append(key, (value as Date).toISOString());
      } else {
        form.append(key, value.toString());
      }
    });
    files.forEach((file) => {
      form.append('files', file);
    });
    return this.http.post<Response>('/rooms', form);
  }

  getProvince(): Observable<Place[]> {
    return this.http
      .get<{ data: Place[] }>(`${this.url}/1/0.htm`, {
        context: new HttpContext()
          .set(SkipApi, true)
          .set(SkipToken, true)
          .set(SkipPreloader, true),
      })
      .pipe(map((response) => response.data));
  }
  getDistrict(provinceId: string): Observable<Place[]> {
    return this.http
      .get<{ data: Place[] }>(`${this.url}/2/${provinceId}.htm`, {
        context: new HttpContext()
          .set(SkipApi, true)
          .set(SkipToken, true)
          .set(SkipPreloader, true),
      })
      .pipe(map((response) => response.data));
  }
  getWard(districtId: string): Observable<Place[]> {
    return this.http
      .get<{ data: Place[] }>(`${this.url}/3/${districtId}.htm`, {
        context: new HttpContext()
          .set(SkipApi, true)
          .set(SkipToken, true)
          .set(SkipPreloader, true),
      })
      .pipe(map((response) => response.data));
  }
}
