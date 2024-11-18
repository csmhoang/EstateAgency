import { Component, Input } from '@angular/core';
import { Service } from '@features/service/models/service.model';

@Component({
  selector: 'app-service-item',
  standalone: true,
  imports: [],
  templateUrl: './service-item.component.html',
  styleUrl: './service-item.component.scss',
})
export class ServiceItemComponent {
  @Input()
  service?: Service;
}
