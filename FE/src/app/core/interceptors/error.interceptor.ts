import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Response } from '@core/models/response.model';
import { ToastService } from '@shared/services/toast/toast.service';
import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const toastService = inject(ToastService);
  return next(req).pipe(
    catchError((err: HttpErrorResponse) => {
      if (err.error instanceof ErrorEvent) {
        toastService.error(err.error.message);
      } else {
        const res = err.error as Response;
        res.errors.forEach((error) => {
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
