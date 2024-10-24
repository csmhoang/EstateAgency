import {
  APP_INITIALIZER,
  ApplicationConfig,
  provideZoneChangeDetection,
} from '@angular/core';
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

function initializeApp(authService: AuthService) {
  return async () => await authService.autoLogin();
}

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
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
    {
      provide: APP_INITIALIZER,
      useFactory: initializeApp,
      deps: [AuthService],
      multi: true,
    },
  ],
};
