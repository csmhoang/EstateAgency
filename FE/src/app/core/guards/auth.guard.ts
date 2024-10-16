import { inject } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivateFn,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { UserService } from '@core/services/user.service';
import { firstValueFrom, skip, tap } from 'rxjs';

export const isUserAuthenticated: CanActivateFn = async (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
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
