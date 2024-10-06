import { DOCUMENT, isPlatformBrowser } from '@angular/common';
import { AfterViewInit, Component, inject, PLATFORM_ID } from '@angular/core';

declare var Swiper: any;
declare var initSwiperWithCustomPagination: (
  swiperElement: any,
  config: any
) => void;

@Component({
  selector: 'app-apartment-detail-photos',
  standalone: true,
  imports: [],
  templateUrl: './apartment-detail-photos.component.html',
  styleUrl: './apartment-detail-photos.component.scss',
})
export class ApartmentDetailPhotosComponent implements AfterViewInit {
  platformId = inject(PLATFORM_ID);
  document = inject(DOCUMENT);

  ngAfterViewInit(): void {
    if (isPlatformBrowser(this.platformId)) {
      this.initSwiper();
    }
  }

  initSwiper() {
    this.document
      .querySelectorAll('.init-swiper')
      .forEach(function (swiperElement) {
        let config = {
          loop: true,
          speed: 600,
          autoplay: {
            delay: 5000,
          },
          slidesPerView: 'auto',
          navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev',
          },
          pagination: {
            el: '.swiper-pagination',
            type: 'bullets',
            clickable: true,
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
