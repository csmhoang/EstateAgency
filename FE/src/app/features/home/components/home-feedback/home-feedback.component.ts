import { DOCUMENT, isPlatformBrowser } from '@angular/common';
import { Component, AfterViewInit, inject, PLATFORM_ID } from '@angular/core';
import { Evaluation } from '@features/home/models/evaluation.model';

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
  evaluations: Evaluation[] = [
    {
      name: 'Trần Minh Đức',
      avatar: 'assets/img/populations/customer_1.jpg',
      rating: 5,
      comment:
        'Sau 6 tháng ở đây, tôi hoàn toàn hài lòng với chất lượng phòng trọ và dịch vụ. Phòng rộng rãi, thoáng mát với đầy đủ nội thất cơ bản. Đặc biệt ấn tượng với đội ngũ bảo trì chuyên nghiệp, bất cứ khi nào gặp vấn đề về điện nước đều được xử lý nhanh chóng trong vòng 30 phút.',
    },
    {
      name: 'Phạm Thu Trang',
      avatar: 'assets/img/populations/customer_2.jpg',
      rating: 4,
      comment:
        'Điểm cộng lớn nhất là vị trí đắc địa, gần trung tâm thương mại và các tiện ích. Hệ thống an ninh với camera 24/7 và bảo vệ trực đêm giúp tôi hoàn toàn yên tâm. Tuy nhiên, chi phí dịch vụ hàng tháng hơi cao so với mặt bằng chung, dù phải công nhận chất lượng dịch vụ xứng đáng với giá tiền.',
    },
    {
      name: 'Lê Minh Khôi',
      avatar: 'assets/img/populations/customer_3.jpg',
      rating: 5,
      comment:
        'Là người làm việc từ xa, tôi rất ưng ý với đường truyền internet ổn định và không gian làm việc yên tĩnh ở đây. Dịch vụ dọn phòng hàng tuần cực kỳ chuyên nghiệp, nhân viên còn chu đáo phun khử khuẩn và thay ga giường định kỳ. Khu vực sinh hoạt chung được thiết kế đẹp và tiện nghi, là nơi lý tưởng để gặp gỡ bạn bè cuối tuần.',
    },
  ];

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
