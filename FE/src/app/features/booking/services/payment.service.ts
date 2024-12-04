import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Result } from '@core/models/result.model';

@Injectable({
  providedIn: 'root',
})
export class PaymentService {
  constructor(private http: HttpClient) {}

  insert(invoiceId: string) {
    return this.http.post<Result>(`/payments?invoiceId=${invoiceId}`, null);
  }
}
