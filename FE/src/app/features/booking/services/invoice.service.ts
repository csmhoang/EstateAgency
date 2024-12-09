import { HttpClient, HttpContext, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SkipPreloader } from '@core/interceptors/skip.resolver';
import { Result } from '@core/models/result.model';
import { Invoice } from '../models/invoice.model';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class InvoiceService {
  constructor(private http: HttpClient) {}

  response(id: string, status: string) {
    let params = new HttpParams().set('id', id).set('status', status);

    return this.http.put<Result>('/invoices/response', null, {
      params,
      context: new HttpContext().set(SkipPreloader, true),
    });
  }

  getInvoice(bookingId: string) {
    return this.http
      .get<Result<Invoice>>(`/invoices/byBooking/${bookingId}`)
      .pipe(map((response) => response.data));
  }
}
