import { HttpClient, HttpContext } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '@core/models/user.model';
import {
  BehaviorSubject,
  distinctUntilChanged,
  map,
  Observable,
  shareReplay,
  tap,
} from 'rxjs';
import { CookieService } from './cookie.service';
import { Response } from '@core/models/response.model';
import { SkipPreloader } from '@shared/components/preloader/skip-preloader.component';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private currentUserSubject = new BehaviorSubject<User | null>(null);
  public currentUser = this.currentUserSubject
    .asObservable()
    .pipe(distinctUntilChanged());

  public isAuthenticated = this.currentUser.pipe(map((user) => !!user));

  constructor(
    private readonly http: HttpClient,
    private readonly cookie: CookieService
  ) {}

  getCurrentUser(isHideLoading: boolean = false): Observable<Response<User>> {
    return this.http
      .get<Response<User>>('/authentication/current', {
        context: new HttpContext().set(SkipPreloader, isHideLoading),
      })
      .pipe(
        tap({
          next: (res) => {
            this.setAuth(res.data);
          },
          error: () => this.purAuth(),
        }),
        shareReplay(1)
      );
  }

  setAuth(user: User): void {
    this.currentUserSubject.next(user);
  }

  purAuth(): void {
    this.cookie.remove();
    this.currentUserSubject.next(null);
  }
}
