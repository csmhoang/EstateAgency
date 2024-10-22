import { APP_INITIALIZER, ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import {
  provideHttpClient,
  withFetch,
  withInterceptors,
} from '@angular/common/http';
import { preloaderInterceptor } from '@core/interceptors/preloader.interceptor';
import { apiInterceptor } from '@core/interceptors/api.interceptor';
import { tokenInterceptor } from '@core/interceptors/token.interceptor';
import { AuthService } from '@core/auth/services/auth.service';
import { errorInterceptor } from '@core/interceptors/error.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideClientHydration(),
    provideAnimationsAsync(),
    provideHttpClient(
      withFetch(),
      withInterceptors([
        apiInterceptor,
        tokenInterceptor,
        preloaderInterceptor,
        errorInterceptor,
      ])
    ),
    AuthService,
    {
      provide: APP_INITIALIZER,
      useFactory: (authService: AuthService) => () => authService.autoLogin(),
      deps: [AuthService],
      multi: true,
    },
  ],
};
