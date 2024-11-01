import { DestroyRef, inject, Injectable } from '@angular/core';
import { AuthService } from '@core/auth/services/auth.service';
import { UserService } from './user.service';
import { catchError, forkJoin, map, of } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Injectable({
  providedIn: 'root',
})
export class InitService {
  destroyRef = inject(DestroyRef);

  constructor(
    private authService: AuthService,
    private userService: UserService
  ) {}

  init() {
    const autoLogin = this.authService.autoLogin().pipe(
      takeUntilDestroyed(this.destroyRef),
      catchError(() => of(null))
    );
    const currentUser = this.userService.init(true).pipe(
      takeUntilDestroyed(this.destroyRef),
      catchError(() => of(null))
    );
    return forkJoin({
      autoLogin,
      currentUser,
    }).pipe(takeUntilDestroyed(this.destroyRef));
  }
}
