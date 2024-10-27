import { HttpClient, HttpContext, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import {
  SkipApi,
  SkipToken,
  SkipPreloader,
} from '@core/interceptors/skip.resolver';
import { Place } from '@features/post/models/place.model';
import { Observable, map } from 'rxjs';
import { Room } from '../models/room.model';
import { Result } from '@core/models/result.model';
import { PageData } from '@core/models/page-data.model';
import { RoomParams } from '../models/room-params.model';

@Injectable({
  providedIn: 'root',
})
export class ApartmentService {
  private http = inject(HttpClient)
  private url = 'https://esgoo.net/api-tinhthanh';

  getList(specParams: RoomParams): Observable<PageData<Room[]>> {
    let params = new HttpParams();
    Object.entries(specParams).forEach(([key, value]) => {
      if (value) {
        params = params.set(key, value);
      }
    });
    return this.http
      .get<Result<PageData<Room[]>>>('/rooms/list', { params })
      .pipe(map((response) => response.data));
  }

  getById(id: string) {
    return this.http
      .get<Result<Room>>(`/rooms?id=${id}`)
      .pipe(map((response) => response.data));
  }

  insert(room: Room, files: File[]): Observable<Result> {
    debugger;
    const form = new FormData();
    Object.entries(room).forEach(([key, value]) => {
      form.append(key, value.toString());
    });
    files.forEach((file) => {
      form.append('files', file);
    });
    return this.http.post<Result>('/rooms', form);
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
