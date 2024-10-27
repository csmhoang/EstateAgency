import { HttpClient, HttpContext } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Result } from '@core/models/result.model';
import { Secret } from '@core/models/secret.model';
import { CookieService } from '@core/services/cookie.service';
import { UserService } from '@core/services/user.service';
import { map, Observable, of, tap } from 'rxjs';
import { Login } from '../models/login.model';
import { Register } from '../models/register.model';
import { SkipPreloader } from '@core/interceptors/skip.resolver';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(
    private http: HttpClient,
    private router: Router,
    private cookie: CookieService,
    private userService: UserService
  ) {}

  login(
    credentials: Login,
    isHideLoading: boolean = false
  ): Observable<Result<Secret>> {
    return this.http
      .post<Result<Secret>>('/authentication/login', credentials, {
        context: new HttpContext().set(SkipPreloader, isHideLoading),
      })
      .pipe(
        tap((response) => {
          if (response.success) {
            const secret: Secret = {
              ...credentials,
              ...response.data,
            };
            this.cookie.save(secret, 7);
            this.userService.init(isHideLoading).subscribe({
              next: (user) => {
                const roles = user.data?.roles;
                if (roles?.includes('admin')) {
                  void this.router.navigate(['/admin']);
                } else if (roles?.includes('landlord')) {
                  void this.router.navigate(['/lessor']);
                } else {
                  void this.router.navigate(['/']);
                }
              },
            });
          }
        })
      );
  }

  register(
    credentials: Register,
    isHideLoading: boolean = false
  ): Observable<Result> {
    return this.http.post<Result>('/authentication/register', credentials, {
      context: new HttpContext().set(SkipPreloader, isHideLoading),
    });
  }

  logout(): void {
    this.userService.purAuth();
    void this.router.navigate(['/']);
  }

  autoLogin() {
    if (this.cookie.get('isRemember') === 'true') {
      const credentials: Login = {
        email: this.cookie.get('email'),
        password: this.cookie.get('password'),
        isRemember: true,
      };
      return this.http
        .post<Result<Secret>>('/authentication/login', credentials)
        .pipe(
          map((response)=>{
            if (response.success) {
              const secret: Secret = {
                ...credentials,
                ...response.data,
              };
              this.cookie.save(secret, 7);
            }
          }),
          tap({
            error: () => {
              this.cookie.remove();
            },
          })
        );
    }
    return of(null);
  }
}
