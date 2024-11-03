import { Component, inject, Input } from '@angular/core';
import { ApartmentDetailPhotosComponent } from '../apartment-detail-photos/apartment-detail-photos.component';
import {
  Category,
  ConditionRoom,
  Interior,
  Room,
} from '@features/apartment/models/room.model';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-apartment-view',
  standalone: true,
  imports: [ApartmentDetailPhotosComponent],
  templateUrl: './apartment-view.component.html',
  styleUrl: './apartment-view.component.scss',
})
export class ApartmentViewComponent {
  @Input() data!: Room;
  activeModal = inject(NgbActiveModal);
  conditionFilter = ConditionRoom;
  categoryFilter = Category;
  interiorFilter = Interior;

  decline() {
    this.activeModal.dismiss(false);
  }
}
