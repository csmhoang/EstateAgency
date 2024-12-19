import { Component, Input } from '@angular/core';
import { ApartmentDetailPhotosComponent } from '../apartment-detail-photos/apartment-detail-photos.component';
import { Post } from '@features/post/models/post.model';

@Component({
  selector: 'app-apartment-detail-tab',
  standalone: true,
  imports: [ApartmentDetailPhotosComponent],
  templateUrl: './apartment-detail-tab.component.html',
  styleUrl: './apartment-detail-tab.component.scss',
})
export class ApartmentDetailTabComponent {
  @Input() post?: Post | null;
}
