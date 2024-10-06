import { DOCUMENT, isPlatformBrowser } from '@angular/common';
import { AfterViewInit, Component, inject, PLATFORM_ID } from '@angular/core';

@Component({
  selector: 'app-home-slide',
  standalone: true,
  imports: [],
  templateUrl: './home-slide.component.html',
  styleUrl: './home-slide.component.scss',
})
export class HomeSlideComponent implements AfterViewInit {
  platformId = inject(PLATFORM_ID);
  document = inject(DOCUMENT);

  ngAfterViewInit(): void {
    if (isPlatformBrowser(this.platformId)) {
      this.carousel();
    }
  }
  carousel() {
    this.document
      .querySelectorAll<HTMLElement>('.carousel-indicators')
      .forEach((carouselIndicator) => {
        const carousel = carouselIndicator.closest<HTMLElement>('.carousel');
        if (!carousel) return;

        carousel
          .querySelectorAll<HTMLElement>('.carousel-item')
          .forEach((carouselItem, index) => {
            const carouselId = carousel.id;
            if (index === 0) {
              carouselIndicator.innerHTML += `<li data-bs-target="#${carouselId}" data-bs-slide-to="${index}" class="active"></li>`;
            } else {
              carouselIndicator.innerHTML += `<li data-bs-target="#${carouselId}" data-bs-slide-to="${index}"></li>`;
            }
          });
      });
  }
}
