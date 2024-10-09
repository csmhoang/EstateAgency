import { Component } from '@angular/core';
import { FooterComponent } from '@core/layout/footer/footer.component';
import { HeaderComponent } from '@core/layout/header/header.component';

@Component({
  selector: 'app-service-detail',
  standalone: true,
  imports: [HeaderComponent, FooterComponent],
  templateUrl: './service-detail.component.html',
  styleUrl: './service-detail.component.scss',
})
export class ServiceDetailComponent {}
