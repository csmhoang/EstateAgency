import { DestroyRef, inject, Injectable } from '@angular/core';
import { AuthService } from '@core/auth/services/auth.service';
import { UserService } from './user.service';
import { catchError, concat, delay, finalize, forkJoin, of, tap } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { CookieService } from './cookie.service';
import { CartService } from '@features/Cart/services/cart.service';
import { PreloaderService } from '@shared/services/preloader/preloader.service';

@Injectable({
  providedIn: 'root',
})
export class InitService {
  destroyRef = inject(DestroyRef);
  preloaderService = inject(PreloaderService);
  constructor(
    private authService: AuthService,
    private userService: UserService,
    private cookie: CookieService,
    private cartService: CartService
  ) {}

  init() {
    const autoLogin = this.authService.autoLogin().pipe(
      takeUntilDestroyed(this.destroyRef),
      catchError(() => of(null))
    );
    const token = this.cookie.get('token');
    const currentUser = token
      ? this.userService.init(true).pipe(
          takeUntilDestroyed(this.destroyRef),
          catchError(() => of(null))
        )
      : of(null);

    const currentCart = token
      ? this.cartService.init(true).pipe(
          takeUntilDestroyed(this.destroyRef),
          catchError(() => of(null))
        )
      : of(null);
    this.preloaderService.loadingOn();
    return concat(
      autoLogin,
      forkJoin({
        currentUser,
        currentCart,
      })
    ).pipe(
      takeUntilDestroyed(this.destroyRef),
      finalize(() => {
        this.preloaderService.loadingOff();
      })
    );
  }
}
