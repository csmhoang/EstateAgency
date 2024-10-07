import { Component } from '@angular/core';
import { LikeComponent } from '@shared/components/like/like.component';

@Component({
  selector: 'app-apartment-primary-info',
  standalone: true,
  imports: [LikeComponent],
  templateUrl: './apartment-primary-info.component.html',
  styleUrl: './apartment-primary-info.component.scss',
})
export class ApartmentPrimaryInfoComponent {}
