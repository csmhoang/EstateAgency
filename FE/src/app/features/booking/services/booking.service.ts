import { Injectable, signal } from '@angular/core';
import { Booking } from '../models/booking.model';
import { Result } from '@core/models/result.model';
import { HttpClient, HttpContext, HttpParams } from '@angular/common/http';
import { SkipPreloader } from '@core/interceptors/skip.resolver';
import { TakeMiniLoad } from '@core/interceptors/take.resolver';
import { PageData } from '@core/models/page-data.model';
import { tap, Observable, map } from 'rxjs';
import { SpecBookingParams } from '../models/spec-booking-params.model';

@Injectable({
  providedIn: 'root',
})
export class BookingService {
  pageSignal = signal<PageData<Booking[]> | null>(null);
  public page = this.pageSignal.asReadonly();
  public specParams = signal<SpecBookingParams>({});

  constructor(private http: HttpClient) {}

  loadData(isDisplayMiniLoading: boolean = true) {
    return this.getList(this.specParams(), isDisplayMiniLoading).pipe(
      tap({
        next: (page) => {
          if (page) {
            this.pageSignal.set(page);
          }
        },
      })
    );
  }

  getList(
    specParams: SpecBookingParams,
    isDisplayMiniLoading: boolean = false
  ): Observable<PageData<Booking[]>> {
    let params = new HttpParams();
    Object.entries(specParams).forEach(([key, value]) => {
      if (value) {
        params = params.set(key, value.toString());
      }
    });

    return this.http
      .get<Result<PageData<Booking[]>>>('/bookings/list', {
        params,
        context: new HttpContext()
          .set(SkipPreloader, true)
          .set(TakeMiniLoad, isDisplayMiniLoading),
      })
      .pipe(map((response) => response.data));
  }

  insert() {
    return this.http.post<Result>('/bookings', null);
  }

  response(id: string, status: string) {
    let params = new HttpParams().set('id', id).set('status', status);

    return this.http.put<Result>('/bookings/response', null, {
      params,
      context: new HttpContext().set(SkipPreloader, true),
    });
  }
}
