import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Result } from '@core/models/result.model';
import { ToastService } from '@shared/services/toast/toast.service';
import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const toastService = inject(ToastService);
  return next(req).pipe(
    catchError((err: HttpErrorResponse) => {
      if (err.error instanceof HttpErrorResponse) {
        toastService.error(err.error.message);
      } else {
        const res = err.error as Result;
        res.errors?.forEach((error) => {
          toastService.error(error);
        });
        if (res.messages) {
          toastService.warn(res.messages);
        }
      }
      return throwError(() => err);
    })
  );
};
