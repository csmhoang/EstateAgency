import { Component } from '@angular/core';
import { PaginationComponent } from '@shared/components/pagination/pagination.component';
import { PostRootItemComponent } from '../post-root-item/post-root-item.component';

@Component({
  selector: 'app-post-root-list',
  standalone: true,
  imports: [PostRootItemComponent, PaginationComponent],
  templateUrl: './post-root-list.component.html',
  styleUrl: './post-root-list.component.scss'
})
export class PostRootListComponent {

}
