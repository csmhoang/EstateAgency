import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  constructor(private http: HttpClient) {}

  getAll<T>(url: string): Observable<T[]> {
    return this.http.get<T[]>(`${url}`);
  }

  getById<T>(url: string): Observable<T> {
    return this.http.get<T>(`${url}`);
  }

  create<T>(url: string, data: T): Observable<T> {
    return this.http.post<T>(`${url}`, data);
  }

  update<T>(url: string, data: T): Observable<T> {
    return this.http.put<T>(`${url}`, data);
  }

  delete<T>(url: string, id: string): Observable<T> {
    return this.http.delete<T>(`${url}?id=${id}`);
  }
}
