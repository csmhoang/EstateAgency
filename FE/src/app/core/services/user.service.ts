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
import { Result } from '@core/models/result.model';
import { SkipPreloader } from '@core/interceptors/skip.resolver';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private currentUserSubject = new BehaviorSubject<User | null>(null);

  public currentUser = this.currentUserSubject
    .asObservable()
    .pipe(distinctUntilChanged());

  public isAuthenticated = this.currentUser.pipe(map((user) => !!user));

  public isLandlord = this.currentUser.pipe(
    map((user) => !!user?.roles?.includes('landlord'))
  );

  public isAdmin = this.currentUser.pipe(
    map((user) => !!user?.roles?.includes('admin'))
  );

  constructor(
    private readonly http: HttpClient,
    private readonly cookie: CookieService
  ) {}

  init(isHideLoading: boolean = false): Observable<Result<User>> {
    return this.http
      .get<Result<User>>('/authentication/current', {
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

  update(id: string, user: User) {
    return this.http.put<Result>(`/users?id=${id}`, user);
  }

  avatar(id: string, file: File) {
    const form = new FormData();
    form.append('file', file);
    return this.http.put<Result>(`/users/avatar?id=${id}`, form);
  }
}
