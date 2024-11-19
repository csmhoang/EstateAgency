import { Injectable } from '@angular/core';
import { Result } from '@core/models/result.model';
import { HttpClient } from '@angular/common/http';
import { Reservation } from '../models/reservation.model';

@Injectable({
  providedIn: 'root'
})
export class ReservationService {

  constructor(private http: HttpClient) {}

  insert(reservation: Reservation) {
    return this.http.post<Result>('/reservations', reservation);
  }
}
