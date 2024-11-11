import { HttpClient, HttpContext, HttpParams } from '@angular/common/http';
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
import { PageData } from '@core/models/page-data.model';
import { SpecUserParams } from '@core/models/spec-user-params.model';
import { TakeMiniLoad } from '@core/interceptors/take.resolver';

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
    map(
      (user) =>
        !!user?.userRoles
          ?.map((userRole) => userRole.role?.name)
          .includes('landlord')
    )
  );

  public isAdmin = this.currentUser.pipe(
    map(
      (user) =>
        !!user?.userRoles
          ?.map((userRole) => userRole.role?.name)
          .includes('admin')
    )
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

  getList(
    specUserParams: SpecUserParams,
    isDisplayMiniLoading: boolean = false
  ): Observable<PageData<User[]>> {
    let params = new HttpParams();
    Object.entries(specUserParams).forEach(([key, value]) => {
      if (value !== undefined && value !== null && value !== '') {
        params = params.set(key, value.toString());
      }
    });

    return this.http
      .get<Result<PageData<User[]>>>('/users/list', {
        params,
        context: new HttpContext()
          .set(SkipPreloader, true)
          .set(TakeMiniLoad, isDisplayMiniLoading),
      })
      .pipe(map((response) => response.data));
  }

  getDetail(id: string) {
    return this.http
      .get<Result<User>>(`/users/detail/${id}`)
      .pipe(map((response) => response.data));
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
