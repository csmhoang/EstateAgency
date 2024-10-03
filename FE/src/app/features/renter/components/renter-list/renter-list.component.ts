import { Component } from '@angular/core';
import { RenterItemComponent } from '../renter-item/renter-item.component';

@Component({
  selector: 'app-renter-list',
  standalone: true,
  imports: [RenterItemComponent],
  templateUrl: './renter-list.component.html',
  styleUrl: './renter-list.component.scss',
})
export class RenterListComponent {}
