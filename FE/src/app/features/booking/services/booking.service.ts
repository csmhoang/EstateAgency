import { Injectable, signal } from '@angular/core';
import { Booking } from '../models/booking.model';
import { Result } from '@core/models/result.model';
import { HttpClient, HttpContext, HttpParams } from '@angular/common/http';
import { SkipPreloader } from '@core/interceptors/skip.resolver';
import { TakeMiniLoad } from '@core/interceptors/take.resolver';
import { PageData } from '@core/models/page-data.model';
import { tap, Observable, map } from 'rxjs';
import { SpecBookingParams } from '../models/spec-booking-params.model';
import { Invoice } from '../models/invoice.model';

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

  insert(booking: Booking) {
    return this.http.post<Result>('/bookings', booking);
  }
  
  cancel(id: string) {
    return this.http.delete<Result>(`/bookings/cancel?id=${id}`);
  }

  refuse(id: string, rejectionReason: string) {
    return this.http.put<Result>(
      `/bookings/refuse?id=${id}&rejectionReason=${rejectionReason}`,
      null
    );
  }

  accept(id: string) {
    return this.http.put<Result>(`/bookings/accept?id=${id}`, null);
  }

  getInvoice(bookingId: string) {
    return this.http
      .get<Result<Invoice>>(`/invoices/byBooking/${bookingId}`)
      .pipe(map((response) => response.data));
  }
}
