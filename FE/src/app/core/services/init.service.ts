import { DestroyRef, inject, Injectable } from '@angular/core';
import { AuthService } from '@core/auth/services/auth.service';
import { UserService } from './user.service';
import { catchError, forkJoin, of, tap } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { CookieService } from './cookie.service';
import { PresenceService } from './presence.service';

@Injectable({
  providedIn: 'root',
})
export class InitService {
  destroyRef = inject(DestroyRef);

  constructor(
    private authService: AuthService,
    private userService: UserService,
    private cookie: CookieService,
    private presenceService: PresenceService
  ) {}

  init() {
    const autoLogin = this.authService.autoLogin().pipe(
      takeUntilDestroyed(this.destroyRef),
      catchError(() => of(null))
    );
    const token = this.cookie.get('token');
    const currentUser = token
      ? this.userService.init(true).pipe(
          tap(() => {
            this.presenceService.createHubConnection();
          }),
          takeUntilDestroyed(this.destroyRef),
          catchError(() => of(null))
        )
      : of(null);
    return forkJoin({
      autoLogin,
      currentUser,
    }).pipe(takeUntilDestroyed(this.destroyRef));
  }
}
