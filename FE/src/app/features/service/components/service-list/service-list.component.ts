import { Component } from '@angular/core';
import { ServiceItemComponent } from '../service-item/service-item.component';

@Component({
  selector: 'app-service-list',
  standalone: true,
  imports: [ServiceItemComponent],
  templateUrl: './service-list.component.html',
  styleUrl: './service-list.component.scss'
})
export class ServiceListComponent {

}
