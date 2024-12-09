import { Injectable, signal } from '@angular/core';
import { BookingDetail } from '../models/booking-detail.model';
import { HttpParams, HttpContext, HttpClient } from '@angular/common/http';
import { SkipPreloader } from '@core/interceptors/skip.resolver';
import { TakeMiniLoad } from '@core/interceptors/take.resolver';
import { PageData } from '@core/models/page-data.model';
import { Result } from '@core/models/result.model';
import { tap, Observable, map } from 'rxjs';
import { SpecBookingDetailParams } from '../models/spec-booking-params.model';

@Injectable({
  providedIn: 'root',
})
export class BookingDetailService {
  pageSignal = signal<PageData<BookingDetail[]> | null>(null);
  public page = this.pageSignal.asReadonly();
  public specParams = signal<SpecBookingDetailParams>({});

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
    specParams: SpecBookingDetailParams,
    isDisplayMiniLoading: boolean = false
  ): Observable<PageData<BookingDetail[]>> {
    let params = new HttpParams();
    Object.entries(specParams).forEach(([key, value]) => {
      if (value) {
        params = params.set(key, value.toString());
      }
    });

    return this.http
      .get<Result<PageData<BookingDetail[]>>>(
        '/bookings/rented-booking-details',
        {
          params,
          context: new HttpContext()
            .set(SkipPreloader, true)
            .set(TakeMiniLoad, isDisplayMiniLoading),
        }
      )
      .pipe(map((response) => response.data));
  }

  responseDetail(id: string, status: string, rejectionReason?: string) {
    let params = new HttpParams().set('id', id).set('status', status);
    if (rejectionReason) {
      params = params.set('rejectionReason', rejectionReason);
    }

    return this.http.put<Result>('/bookings/response-detail', null, {
      params,
      context: new HttpContext().set(SkipPreloader, true),
    });
  }
}
