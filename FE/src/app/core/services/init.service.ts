import { inject, Injectable } from '@angular/core';
import { AuthService } from '@core/auth/services/auth.service';
import { UserService } from './user.service';
import { catchError, forkJoin, map, of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class InitService {
  private authService = inject(AuthService);
  private userService = inject(UserService);

  init() {
    const autoLogin = this.authService.autoLogin();
    const currentUser = this.userService.init().pipe(
      catchError(() => of(null))
    );
    return forkJoin({
      autoLogin,
      currentUser,
    });
  }
}
