import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Result } from '@core/models/result.model';
import { map } from 'rxjs';
import { Lease } from '../models/lease.model';

@Injectable({
  providedIn: 'root',
})
export class LeaseService {
  constructor(private http: HttpClient) {}

  getByBookingId(bookingId: string) {
    return this.http
      .get<Result<Lease>>(`/leases/bookingId/${bookingId}`)
      .pipe(map((response) => response.data));
  }

  insert(bookingId: string, lease: Lease) {
    return this.http.post<Result>(`/bookings?bookingId=${bookingId}`, lease);
  }
}
