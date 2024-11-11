import { inject, DestroyRef } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ResolveFn } from '@angular/router';
import { User } from '@core/models/user.model';
import { UserService } from '@core/services/user.service';
import { firstValueFrom, catchError, of } from 'rxjs';

export const lessorDetailResolver: ResolveFn<User | null> = (route, state) => {
  const destroyRef = inject(DestroyRef);
  const userId = route.paramMap.get('id');
  if (!userId) {
    return null;
  }
  const userService = inject(UserService);
  return firstValueFrom(
    userService.getDetail(userId).pipe(
      takeUntilDestroyed(destroyRef),
      catchError(() => of(null))
    )
  );
};
