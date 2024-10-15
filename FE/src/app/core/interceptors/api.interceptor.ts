import { HttpInterceptorFn } from '@angular/common/http';
import { environment } from '@environment/environment.development';

export const apiInterceptor: HttpInterceptorFn = (req, next) => {
  const env = environment;
  const apiReq = req.clone({ url: `${env.apiRoot}/api/v1${req.url}` });
  return next(apiReq);
};
