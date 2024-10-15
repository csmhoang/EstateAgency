import { inject } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivateFn,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { UserService } from '@core/services/user.service';

export const isUserAuthenticated: CanActivateFn = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
  const userService = inject(UserService);
  const router = inject(Router);
  if (userService.isAuthenticated) {
    return true;
  } else {
    return router.parseUrl('/login');
  }
};

export const isUserlogined: CanActivateFn = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
  const userService = inject(UserService);
  const router = inject(Router);
  if (!userService.isAuthenticated) {
    return true;
  } else {
    return router.parseUrl('/');
  }
};
