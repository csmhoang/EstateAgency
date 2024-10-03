import { Component } from '@angular/core';
import { FAQComponent } from '@features/faq/faq.component';
import { FooterComponent } from '@features/footer/footer.component';
import { HeaderComponent } from '@features/header/header.component';
import { ServiceListComponent } from '@features/service/components/service-list/service-list.component';
import { ScrollTopComponent } from '@shared/components/scroll-top/scroll-top.component';

@Component({
  selector: 'app-service-page',
  standalone: true,
  imports: [
    HeaderComponent,
    FooterComponent,
    ScrollTopComponent,
    ServiceListComponent,
    FAQComponent,
  ],
  templateUrl: './service-page.component.html',
  styleUrl: './service-page.component.scss',
})
export class ServicePageComponent {}
