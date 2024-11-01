import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { UserService } from '@core/services/user.service';
import { firstValueFrom, map } from 'rxjs';

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

export const isLandlord: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const userService = inject(UserService);
  return userService.isLandlord.pipe(
    map((isLandlord) => {
      if (isLandlord) {
        return true;
      }
      return router.parseUrl('/');
    })
  );
};
