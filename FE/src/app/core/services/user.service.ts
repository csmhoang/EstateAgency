import { HttpClient, HttpContext, HttpParams } from '@angular/common/http';
import { computed, Injectable, signal } from '@angular/core';
import { User } from '@core/models/user.model';
import { map, Observable, shareReplay, tap } from 'rxjs';
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
  private currentUserSignal = signal<User | null>(null);
  public currentUser = this.currentUserSignal.asReadonly();

  public isAuthenticated = computed(() => !!this.currentUser());

  public isTenant = computed(
    () =>
      !!this.currentUser()
        ?.userRoles?.map((userRole) => userRole.role?.name)
        .includes('tenant')
  );

  public isLandlord = computed(
    () =>
      !!this.currentUser()
        ?.userRoles?.map((userRole) => userRole.role?.name)
        .includes('landlord')
  );

  public isAdmin = computed(
    () =>
      !!this.currentUser()
        ?.userRoles?.map((userRole) => userRole.role?.name)
        .includes('admin')
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

  getFamous() {
    return this.http
      .get<Result<User[]>>('/users/famous', {
        context: new HttpContext().set(SkipPreloader, true),
      })
      .pipe(map((response) => response.data));
  }

  getDetail(id: string) {
    return this.http
      .get<Result<User>>(`/users/detail/${id}`)
      .pipe(map((response) => response.data));
  }

  follow(followerId: string, followeeId: string, isFollow: boolean) {
    let params = new HttpParams()
      .set('followerId', followerId)
      .set('followeeId', followeeId)
      .set('isFollow', isFollow.toString());
    return this.http.post<Result>('/users/follow', null, {
      params,
      context: new HttpContext().set(SkipPreloader, true),
    });
  }

  setAuth(user: User): void {
    this.currentUserSignal.set(user);
  }

  purAuth(): void {
    this.cookie.remove();
    this.currentUserSignal.set(null);
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
