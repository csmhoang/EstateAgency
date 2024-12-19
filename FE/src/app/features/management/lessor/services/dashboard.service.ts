import { HttpClient, HttpContext, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SkipPreloader } from '@core/interceptors/skip.resolver';
import { Result } from '@core/models/result.model';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class DashboardService {
  constructor(private http: HttpClient) {}

  roomCount() {
    return this.http
      .get<Result<number>>(`/dashboards/room-count`, {
        context: new HttpContext().set(SkipPreloader, true),
      })
      .pipe(map((response) => response.data));
  }

  roomBlankCount() {
    return this.http
      .get<Result<number>>(`/dashboards/room-blank-count`, {
        context: new HttpContext().set(SkipPreloader, true),
      })
      .pipe(map((response) => response.data));
  }

  tenantCount() {
    return this.http
      .get<Result<number>>(`/dashboards/tenant-count`, {
        context: new HttpContext().set(SkipPreloader, true),
      })
      .pipe(map((response) => response.data));
  }

  visitCount(numberOfMonth: number) {
    return this.http
      .get<Result<number>>(
        `/dashboards/visit-count?numberOfMonth=${numberOfMonth}`,
        {
          context: new HttpContext().set(SkipPreloader, true),
        }
      )
      .pipe(map((response) => response.data));
  }

  revenue() {
    return this.http
      .get<Result<number>>(`/dashboards/revenue`, {
        context: new HttpContext().set(SkipPreloader, true),
      })
      .pipe(map((response) => response.data));
  }
}
