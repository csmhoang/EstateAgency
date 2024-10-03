import { Component } from '@angular/core';
import { LikeComponent } from '@shared/components/like/like.component';

@Component({
  selector: 'app-renter-item',
  standalone: true,
  imports: [LikeComponent],
  templateUrl: './renter-item.component.html',
  styleUrl: './renter-item.component.scss',
})
export class RenterItemComponent {}
