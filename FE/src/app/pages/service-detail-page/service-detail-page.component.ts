import { Component } from '@angular/core';
import { FooterComponent } from '@features/footer/footer.component';
import { HeaderComponent } from '@features/header/header.component';
import { ScrollTopComponent } from '@shared/components/scroll-top/scroll-top.component';

@Component({
  selector: 'app-service-detail-page',
  standalone: true,
  imports: [HeaderComponent, ScrollTopComponent, FooterComponent],
  templateUrl: './service-detail-page.component.html',
  styleUrl: './service-detail-page.component.scss'
})
export class ServiceDetailPageComponent {

}
