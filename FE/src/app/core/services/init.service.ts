import { inject, Injectable } from '@angular/core';
import { AuthService } from '@core/auth/services/auth.service';
import { UserService } from './user.service';
import { forkJoin } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class InitService {
  private authService = inject(AuthService);
  private userService = inject(UserService);

  init() {
    const autoLogin = this.authService.autoLogin();
    const currentUser = this.userService.init();
    return forkJoin({
      autoLogin,
      currentUser,
    });
  }
}
