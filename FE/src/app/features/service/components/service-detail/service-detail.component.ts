import { Component } from '@angular/core';
import { ScrollTopComponent } from '../../../../shared/components/scroll-top/scroll-top.component';
import { FooterComponent } from '../../../footer/footer.component';
import { HeaderComponent } from '../../../header/header.component';

@Component({
  selector: 'app-service-detail',
  standalone: true,
  imports: [HeaderComponent, ScrollTopComponent, FooterComponent],
  templateUrl: './service-detail.component.html',
  styleUrl: './service-detail.component.scss'
})
export class ServiceDetailComponent {

}
