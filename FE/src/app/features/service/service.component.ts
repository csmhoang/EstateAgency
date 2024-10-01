import { Component } from '@angular/core';
import { HeaderComponent } from '../header/header.component';
import { ScrollTopComponent } from '../../shared/components/scroll-top/scroll-top.component';
import { FooterComponent } from '../footer/footer.component';
import { ServiceListComponent } from './components/service-list/service-list.component';
import { FAQComponent } from '../faq/faq.component';

@Component({
  selector: 'app-service',
  standalone: true,
  imports: [
    HeaderComponent,
    FooterComponent,
    ScrollTopComponent,
    ServiceListComponent,
    FAQComponent
  ],
  templateUrl: './service.component.html',
  styleUrl: './service.component.scss',
})
export class ServiceComponent {}
