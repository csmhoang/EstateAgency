import {
  HttpHandlerFn,
  HttpInterceptorFn,
  HttpRequest,
} from '@angular/common/http';
import { PreloaderService } from './preloader.service';
import { inject } from '@angular/core';
import { finalize } from 'rxjs';
import { SkipPreloader } from './skip-preloader.component';

export const preloaderInterceptor: HttpInterceptorFn = (
  req: HttpRequest<unknown>,
  next: HttpHandlerFn
) => {
  if (req.context.get(SkipPreloader)) {
    return next(req);
  }
  const preloaderService = inject(PreloaderService);
  preloaderService.loadingOn();
  return next(req).pipe(
    finalize(() => {
      preloaderService.loadingOff();
    })
  );
};
