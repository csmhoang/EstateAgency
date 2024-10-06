import { DOCUMENT, isPlatformBrowser } from '@angular/common';
import { Component, AfterViewInit, inject, PLATFORM_ID } from '@angular/core';

declare var Swiper: any;
declare var initSwiperWithCustomPagination: (
  swiperElement: any,
  config: any
) => void;

@Component({
  selector: 'app-home-feedback',
  standalone: true,
  imports: [],
  templateUrl: './home-feedback.component.html',
  styleUrl: './home-feedback.component.scss',
})
export class HomeFeedbackComponent implements AfterViewInit {
  platformId = inject(PLATFORM_ID);
  document = inject(DOCUMENT);

  ngAfterViewInit(): void {
    if (isPlatformBrowser(this.platformId)) {
      this.initSwiper();
    }
  }

  initSwiper() {
    this.document.querySelectorAll('.init-swiper').forEach(function (swiperElement) {
      let config = {
        loop: true,
        speed: 600,
        autoplay: {
          delay: 5000,
        },
        slidesPerView: 'auto',
        pagination: {
          el: '.swiper-pagination',
          type: 'bullets',
          clickable: true,
        },
        breakpoints: {
          '320': {
            slidesPerView: 1,
            spaceBetween: 40,
          },
          '1200': {
            slidesPerView: 2,
            spaceBetween: 20,
          },
        },
      };

      if (swiperElement.classList.contains('swiper-tab')) {
        initSwiperWithCustomPagination(swiperElement, config);
      } else {
        new Swiper(swiperElement, config);
      }
    });
  }
}
