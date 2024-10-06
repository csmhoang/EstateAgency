import { Component } from '@angular/core';
import { FAQComponent } from '@features/faq/faq.component';
import { ServiceListComponent } from '@features/service/components/service-list/service-list.component';

@Component({
  selector: 'app-service-page',
  standalone: true,
  imports: [ServiceListComponent, FAQComponent],
  templateUrl: './service-page.component.html',
  styleUrl: './service-page.component.scss',
})
export class ServicePageComponent {}
