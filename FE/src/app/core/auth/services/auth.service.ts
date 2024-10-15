import { HttpClient, HttpContext } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Response } from '@core/models/response.model';
import { Secret } from '@core/models/secret.model';
import { CookieService } from '@core/services/cookie.service';
import { UserService } from '@core/services/user.service';
import { Observable, tap } from 'rxjs';
import { Login } from '../models/login.model';
import { Register } from '../models/register.model';
import { SkipPreloader } from '@shared/components/preloader/skip-preloader.component';

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
  ): Observable<Response<Secret>> {
    return this.http
      .post<Response<Secret>>('/authentication/login', credentials, {
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
            this.userService.getCurrentUser(isHideLoading).subscribe();
          }
        })
      );
  }

  register(credentials: Register): Observable<Response> {
    return this.http
      .post<Response>('/authentication/register', credentials)
      .pipe(
        tap((response) => {
          if (response.success) {
            void this.router.navigate(['/login']);
          }
        })
      );
  }

  logout(): void {
    this.userService.purAuth();
    void this.router.navigate(['/']);
  }
}
