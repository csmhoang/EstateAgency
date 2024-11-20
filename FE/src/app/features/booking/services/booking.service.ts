import { Injectable } from '@angular/core';
import { Booking } from '../models/booking.model';
import { Result } from '@core/models/result.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class BookingService {
  constructor(private http: HttpClient) {}

  insert(booking: Booking) {
    return this.http.post<Result>('/bookings', booking);
  }
}
