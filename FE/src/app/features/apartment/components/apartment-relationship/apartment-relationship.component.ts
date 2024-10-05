import { Component } from '@angular/core';
import { PostListComponent } from '@features/post/components/post-list/post-list.component';
import { PaginationComponent } from '@shared/components/pagination/pagination.component';

@Component({
  selector: 'app-apartment-relationship',
  standalone: true,
  imports: [PostListComponent, PaginationComponent],
  templateUrl: './apartment-relationship.component.html',
  styleUrl: './apartment-relationship.component.scss'
})
export class ApartmentRelationshipComponent {

}
