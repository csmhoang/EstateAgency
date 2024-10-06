import { DOCUMENT, isPlatformBrowser } from '@angular/common';
import { AfterViewInit, Component, inject, PLATFORM_ID } from '@angular/core';

@Component({
  selector: 'app-preloader',
  standalone: true,
  imports: [],
  templateUrl: './preloader.component.html',
  styleUrl: './preloader.component.scss',
})
export class PreloaderComponent implements AfterViewInit {
  platformId = inject(PLATFORM_ID);
  document = inject(DOCUMENT);

  ngAfterViewInit(): void {
    if (isPlatformBrowser(this.platformId)) {
      const preloader = document.querySelector('#preloader');
      if (preloader) {
        preloader.remove();
      }
    }
  }
}
