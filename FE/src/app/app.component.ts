import { isPlatformBrowser } from '@angular/common';
import {
  AfterViewInit,
  Component,
  inject,
  OnInit,
  PLATFORM_ID,
  Renderer2,
} from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Login } from '@core/auth/models/login.model';
import { AuthService } from '@core/auth/services/auth.service';
import { FooterComponent } from '@core/layout/footer/footer.component';
import { HeaderComponent } from '@core/layout/header/header.component';
import { CookieService } from '@core/services/cookie.service';
import { PreloaderComponent } from '@shared/components/preloader/preloader.component';
import { ScrollTopComponent } from '@shared/components/scroll-top/scroll-top.component';
import { ToastComponent } from '@shared/components/toast/toast.component';
import { take } from 'rxjs';

declare var AOS: any;

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    HeaderComponent,
    FooterComponent,
    ScrollTopComponent,
    PreloaderComponent,
    ToastComponent,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements AfterViewInit {
  title = 'Dwello';
  platformId = inject(PLATFORM_ID);
  renderer = inject(Renderer2);
  constructor(
    private cookie: CookieService,
    private authService: AuthService
  ) {}

  ngAfterViewInit() {
    if (isPlatformBrowser(this.platformId)) {
      this.renderer.listen(window, 'load', this.aosInit);

      if (this.cookie.get('isRemember') === 'true') {
        const credentials: Login = {
          email: this.cookie.get('email'),
          password: this.cookie.get('password'),
          isRemember: true,
        };
        this.authService
          .login(credentials)
          .pipe(take(1))
          .subscribe({
            error: () => {
              this.cookie.remove();
            },
          });
      } else {
        this.cookie.remove();
      }
    }
  }

  aosInit() {
    AOS.init({
      duration: 600,
      easing: 'ease-in-out',
      once: true,
      mirror: false,
    });
  }
}
