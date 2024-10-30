import { HttpInterceptorFn } from '@angular/common/http';
import { MiniLoadService } from '@shared/services/mini-load/mini-load.service';
import { SkipMiniLoad } from './skip.resolver';
import { inject } from '@angular/core';
import { delay, finalize, of } from 'rxjs';

export const miniLoadInterceptor: HttpInterceptorFn = (req, next) => {
  if (req.context.get(SkipMiniLoad)) {
    return next(req);
  }
  const miniloadService = inject(MiniLoadService);
  miniloadService.loadingOn();
  return next(req).pipe(
    delay(1000),
    finalize(() => {
      miniloadService.loadingOff()
    })
  );
};
