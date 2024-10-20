import { HttpInterceptorFn } from '@angular/common/http';
import { environment } from '@environment/environment.development';
import { SkipApi } from './skip.resolver';

export const apiInterceptor: HttpInterceptorFn = (req, next) => {
  if (req.context.get(SkipApi)) {
    return next(req);
  }
  const env = environment;
  const apiReq = req.clone({ url: `${env.apiRoot}/api/v1${req.url}` });
  return next(apiReq);
};
