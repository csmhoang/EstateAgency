import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { CookieService } from '@core/services/cookie.service';

export const tokenInterceptor: HttpInterceptorFn = (req, next) => {
  const token = inject(CookieService).get('token');

  const request = req.clone({
    setHeaders: {
      ...(token ? { Authorization: `Bearer ${token}` } : {}),
    },
  });
  return next(request);
};