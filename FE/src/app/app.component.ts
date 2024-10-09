import { isPlatformBrowser } from '@angular/common';
import {
  AfterViewInit,
  Component,
  inject,
  PLATFORM_ID,
  Renderer2,
} from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { FooterComponent } from '@core/layout/footer/footer.component';
import { HeaderComponent } from '@core/layout/header/header.component';
import { PreloaderComponent } from '@shared/components/preloader/preloader.component';
import { ScrollTopComponent } from '@shared/components/scroll-top/scroll-top.component';

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
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements AfterViewInit {
  title = 'Dwello';
  platformId = inject(PLATFORM_ID);
  renderer = inject(Renderer2);

  ngAfterViewInit() {
    if (isPlatformBrowser(this.platformId)) {
      this.renderer.listen(window, 'load', this.aosInit);
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
