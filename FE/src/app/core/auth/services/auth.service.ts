import { HttpClient, HttpContext } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { Router } from '@angular/router';
import { Result } from '@core/models/result.model';
import { Secret } from '@core/models/secret.model';
import { CookieService } from '@core/services/cookie.service';
import { UserService } from '@core/services/user.service';
import { catchError, map, Observable, of, tap } from 'rxjs';
import { Login } from '../models/login.model';
import { Register } from '../models/register.model';
import { SkipPreloader } from '@core/interceptors/skip.resolver';
import { ChangePassword } from '../models/changePassword.model';
import { ResetPassword } from '../models/reset-password.model';
import { PresenceService } from '@core/services/presence.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  public account = signal<Login | null>(null);

  constructor(
    private http: HttpClient,
    private router: Router,
    private cookie: CookieService,
    private userService: UserService,
    private presenceService: PresenceService
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
            this.presenceService.createHubConnection();
            this.userService.init(isHideLoading).subscribe({
              next: (page) => {
                const roles = page.data?.userRoles?.map(
                  (userRole) => userRole.role?.name
                );
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
    this.presenceService.stopHubConnection();
  }

  emailConfirm(email: string, token: string) {
    return this.http.get<Result>(
      `/authentication/email-confirm?email=${email}&token=${token}`,
      {
        context: new HttpContext().set(SkipPreloader, true),
      }
    );
  }

  sendEmailConfirm(email: string) {
    return this.http.get<Result>(
      `/authentication/send-email-confirm?email=${email}`
    );
  }

  resetPassword(credentials: ResetPassword) {
    return this.http.post<Result>(
      '/authentication/reset-password',
      credentials
    );
  }

  sendForgotPassword(email: string) {
    return this.http.get<Result>(
      `/authentication/send-email-forgot?email=${email}`
    );
  }

  changePassWord(credentials: ChangePassword) {
    return this.http.post<Result<Secret>>(
      '/authentication/password',
      credentials
    );
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
          map((response) => {
            if (response.success) {
              const secret: Secret = {
                ...credentials,
                ...response.data,
              };
              this.cookie.save(secret, 7);
              this.presenceService.createHubConnection();
            }
          }),
          tap({
            error: () => {
              this.cookie.remove();
            },
          }),
          catchError(() => of(null))
        );
    }
    return of(null);
  }
}
