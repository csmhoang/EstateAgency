import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { UserService } from '@core/services/user.service';

export const isUserAuthenticated: CanActivateFn = async (route, state) => {
  const router = inject(Router);
  const userService = inject(UserService);

  if (userService.isAuthenticated()) {
    return true;
  } else {
    return router.parseUrl('/');
  }
};

export const isLandlord: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const userService = inject(UserService);
  if (userService.isLandlord()) {
    return true;
  }
  return router.parseUrl('/');
};
