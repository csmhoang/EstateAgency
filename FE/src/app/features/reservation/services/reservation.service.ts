import { Injectable, signal } from '@angular/core';
import { Result } from '@core/models/result.model';
import { HttpClient, HttpContext, HttpParams } from '@angular/common/http';
import { Reservation } from '../models/reservation.model';
import { map, Observable, tap } from 'rxjs';
import { SkipPreloader } from '@core/interceptors/skip.resolver';
import { TakeMiniLoad } from '@core/interceptors/take.resolver';
import { PageData } from '@core/models/page-data.model';
import { SpecReservationParams } from '../models/spec-reservation-params.model';

@Injectable({
  providedIn: 'root',
})
export class ReservationService {
  pageSignal = signal<PageData<Reservation[]> | null>(null);
  public page = this.pageSignal.asReadonly();
  public specParams = signal<SpecReservationParams>({});

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
    specParams: SpecReservationParams,
    isDisplayMiniLoading: boolean = false
  ): Observable<PageData<Reservation[]>> {
    let params = new HttpParams();
    Object.entries(specParams).forEach(([key, value]) => {
      if (value) {
        params = params.set(key, value.toString());
      }
    });

    return this.http
      .get<Result<PageData<Reservation[]>>>('/reservations/list', {
        params,
        context: new HttpContext()
          .set(SkipPreloader, true)
          .set(TakeMiniLoad, isDisplayMiniLoading),
      })
      .pipe(map((response) => response.data));
  }

  insert(reservation: Reservation) {
    return this.http.post<Result>('/reservations', reservation);
  }

  update(id: string, reservation: Reservation) {
    return this.http.put<Result>(`/reservations?id=${id}`, reservation);
  }

  response(id: string, status: string, rejectionReason?: string) {
    let params = new HttpParams().set('id', id).set('status', status);
    if (rejectionReason) {
      params = params.set('rejectionReason', rejectionReason);
    }

    return this.http.put<Result>('/reservations/response', null, {
      params,
      context: new HttpContext().set(SkipPreloader, true),
    });
  }
}
