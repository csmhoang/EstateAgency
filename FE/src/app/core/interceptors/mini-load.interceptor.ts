import { HttpInterceptorFn } from '@angular/common/http';
import { MiniLoadService } from '@shared/services/mini-load/mini-load.service';
import { inject } from '@angular/core';
import { delay, finalize } from 'rxjs';
import { TakeMiniLoad } from './take.resolver';

export const miniLoadInterceptor: HttpInterceptorFn = (req, next) => {
  if (req.context.get(TakeMiniLoad)) {
    const miniloadService = inject(MiniLoadService);
    miniloadService.loadingOn();
    return next(req).pipe(
      delay(2500),
      finalize(() => {
        miniloadService.loadingOff();
      })
    );
  }
  return next(req);
};
