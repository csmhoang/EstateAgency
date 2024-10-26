import { inject } from '@angular/core';
import {
  CanActivateFn,
  Router,
} from '@angular/router';
import { UserService } from '@core/services/user.service';
import { firstValueFrom } from 'rxjs';

export const isUserAuthenticated: CanActivateFn = async (route, state) => {
  const router = inject(Router);
  const userService = inject(UserService);
  const isAuthenticated: boolean = await firstValueFrom(
    userService.isAuthenticated
  );
  if (isAuthenticated) {
    return true;
  } else {
    return router.parseUrl('/');
  }
};

export const isLandlord: CanActivateFn = async (route, state) => {
  const router = inject(Router);
  const userService = inject(UserService);
  const isLandlord: boolean = await firstValueFrom(userService.isLandlord);
  if (isLandlord) {
    return true;
  } else {
    return router.parseUrl('/');
  }
};
