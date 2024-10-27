import { isPlatformBrowser } from '@angular/common';
import {
  AfterViewInit,
  Component,
  inject,
  OnDestroy,
  PLATFORM_ID,
  Renderer2,
} from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { FooterComponent } from '@core/layout/footer/footer.component';
import { HeaderComponent } from '@core/layout/header/header.component';
import { PreloaderComponent } from '@shared/components/preloader/preloader.component';
import { ScrollTopComponent } from '@shared/components/scroll-top/scroll-top.component';
import { ToastComponent } from '@shared/components/toast/toast.component';

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
export class AppComponent implements AfterViewInit, OnDestroy  {
  title = 'Dwello';
  platformId = inject(PLATFORM_ID);
  renderer = inject(Renderer2);
  private loadListener?: () => void; // Biến để giữ tham chiếu đến hàm hủy

  ngAfterViewInit() {
    if (isPlatformBrowser(this.platformId)) {
      if (document.readyState === 'complete') {
        this.aosInit();
      } else {
        this.loadListener = this.renderer.listen(window, 'load', this.aosInit);
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

  ngOnDestroy() {
    if (this.loadListener) {
      this.loadListener(); // Gọi hàm hủy để giải phóng listener
    }
  }
}
