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
import { SpecParams } from '@core/models/spec-params.model';
import { TakeMiniLoad } from '@core/interceptors/take.resolver';
import { BookingDetail } from '@features/booking/models/booking-detail.model';

@Injectable({
  providedIn: 'root',
})
export class RoomService {
  private http = inject(HttpClient);
  private url = 'https://esgoo.net/api-tinhthanh';

  getList(
    specParams: SpecParams,
    isDisplayMiniLoading: boolean = false
  ): Observable<PageData<Room[]>> {
    let params = new HttpParams();
    Object.entries(specParams).forEach(([key, value]) => {
      if (value) {
        params = params.set(key, value.toString());
      }
    });

    return this.http
      .get<Result<PageData<Room[]>>>('/rooms/list', {
        params,
        context: new HttpContext()
          .set(SkipPreloader, true)
          .set(TakeMiniLoad, isDisplayMiniLoading),
      })
      .pipe(map((response) => response.data));
  }

  get(isHideLoading: boolean = false): Observable<Room[]> {
    return this.http
      .get<Result<Room[]>>('/rooms', {
        context: new HttpContext().set(SkipPreloader, isHideLoading),
      })
      .pipe(map((response) => response.data));
  }

  delete(roomId: string) {
    return this.http.delete<Result>(`/rooms?id=${roomId}`);
  }

  update(id: string, room: Room) {
    return this.http.put<Result>(`/rooms?id=${id}`, room);
  }

  deletePhoto(roomId: string, photoId: string) {
    const params = new HttpParams()
      .set('roomId', roomId)
      .set('photoId', photoId);
    return this.http.delete<Result>('/rooms/delete-photo', {
      params,
      context: new HttpContext().set(SkipPreloader, true),
    });
  }

  getById(id: string) {
    return this.http
      .get<Result<Room>>(`/rooms/${id}`)
      .pipe(map((response) => response.data));
  }

  insert(room: Room, files: File[]): Observable<Result> {
    const form = new FormData();
    Object.entries(room).forEach(([key, value]) => {
      if (value) {
        form.append(key, value.toString());
      }
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
