import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { FooterComponent } from '@core/layout/footer/footer.component';
import { HeaderComponent } from '@core/layout/header/header.component';
import { FAQComponent } from '@features/faq/faq.component';
import { ServiceListComponent } from '@features/service/components/service-list/service-list.component';

@Component({
  selector: 'app-service',
  standalone: true,
  imports: [
    HeaderComponent,
    FooterComponent,
    ServiceListComponent,
    FAQComponent,
    RouterLink
  ],
  templateUrl: './service.component.html',
  styleUrl: './service.component.scss',
})
export class ServiceComponent {}
