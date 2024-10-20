import { HttpClient, HttpContext } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SkipApi, SkipPreloader } from '@core/interceptors/skip.resolver';
import { map, Observable } from 'rxjs';
import { Place } from '../models/place.model';

@Injectable({
  providedIn: 'root',
})
export class PostFormService {
  url = 'https://esgoo.net/api-tinhthanh';

  constructor(private http: HttpClient) {}

  getProvince(): Observable<Place[]> {
    return this.http
      .get<{ data: Place[] }>(`${this.url}/1/0.htm`, {
        context: new HttpContext().set(SkipApi, true).set(SkipPreloader, true),
      })
      .pipe(map((response) => response.data));
  }
  getDistrict(provinceId: string): Observable<Place[]> {
    return this.http
      .get<{ data: Place[] }>(`${this.url}/2/${provinceId}.htm`, {
        context: new HttpContext().set(SkipApi, true).set(SkipPreloader, true),
      })
      .pipe(map((response) => response.data));
  }
  getWard(districtId: string): Observable<Place[]> {
    return this.http
      .get<{ data: Place[] }>(`${this.url}/3/${districtId}.htm`, {
        context: new HttpContext().set(SkipApi, true).set(SkipPreloader, true),
      })
      .pipe(map((response) => response.data));
  }
}
