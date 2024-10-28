import {
  HttpHandlerFn,
  HttpInterceptorFn,
  HttpRequest,
} from '@angular/common/http';

import { inject } from '@angular/core';
import { PreloaderService } from '@shared/services/preloader/preloader.service';
import { delay, finalize, of } from 'rxjs';
import { SkipPreloader } from './skip.resolver';

export const preloaderInterceptor: HttpInterceptorFn = (req, next) => {
  if (req.context.get(SkipPreloader)) {
    return next(req);
  }
  const preloaderService = inject(PreloaderService);
  preloaderService.loadingOn();
  return next(req).pipe(
    finalize(() => {
      of(null)
        .pipe(delay(1000))
        .subscribe(() => preloaderService.loadingOff());
    })
  );
};
