import { Component, input } from '@angular/core';
import { ApartmentDetailPhotosComponent } from '../apartment-detail-photos/apartment-detail-photos.component';
import { Category, Condition, Interior, Room } from '@features/apartment/models/room.model';

@Component({
  selector: 'app-apartment-view',
  standalone: true,
  imports: [ApartmentDetailPhotosComponent],
  templateUrl: './apartment-view.component.html',
  styleUrl: './apartment-view.component.scss',
})
export class ApartmentViewComponent {
  room = input.required<Room>();
  condition = Condition;
  category = Category;
  interior = Interior;
}
