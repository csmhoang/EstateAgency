import { Component } from '@angular/core';
import { ServiceItemComponent } from '../service-item/service-item.component';
import { Service } from '@features/service/models/service.model';

@Component({
  selector: 'app-service-list',
  standalone: true,
  imports: [ServiceItemComponent],
  templateUrl: './service-list.component.html',
  styleUrl: './service-list.component.scss',
})
export class ServiceListComponent {
  services: Service[] = [
    {
      name: 'Dọn phòng',
      detail:
        'Dịch vụ dọn phòng chuyên nghiệp, đảm bảo không gian sống của bạn luôn sạch sẽ và thoải mái. Nhân viên được đào tạo kỹ lưỡng sẽ dọn dẹp phòng theo lịch bạn chọn, sử dụng các sản phẩm vệ sinh an toàn và thân thiện với môi trường.',
    },
    {
      name: 'An ninh',
      detail:
        'Hệ thống an ninh 24/7 với camera giám sát, bảo vệ trực chiến và khóa cổng theo giờ. Mỗi phòng được trang bị khóa riêng biệt, đảm bảo an toàn tuyệt đối cho người thuê.',
    },
    {
      name: 'Giữ xe',
      detail:
        'Bãi đỗ xe rộng rãi, có mái che và camera giám sát 24/7. Hệ thống thẻ từ thông minh giúp kiểm soát xe ra vào, đảm bảo an toàn tuyệt đối cho phương tiện.',
    },
    {
      name: 'Bảo trì',
      detail:
        'Đội ngũ kỹ thuật viên sẵn sàng xử lý các sự cố về điện, nước và các thiết bị trong phòng. Bảo trì định kỳ và sửa chữa khẩn cấp nhanh chóng, đảm bảo cuộc sống không bị gián đoạn.',
    },
    {
      name: 'Giặt ủi',
      detail:
        'Trang bị máy giặt công cộng hiện đại và khu phơi đồ riêng biệt. Có thể sử dụng theo nhu cầu với mức phí hợp lý, tiết kiệm thời gian và công sức cho người thuê.',
    },
    {
      name: 'Tiện ích chung',
      detail:
        'Không gian sinh hoạt chung tiện nghi với khu bếp, phòng sinh hoạt cộng đồng và sân vườn. Thiết kế hiện đại, thoáng mát, tạo môi trường sống thoải mái cho cộng đồng cư dân.',
    },
  ];
}
