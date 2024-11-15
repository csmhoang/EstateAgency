import { Component, Input } from '@angular/core';
import { User } from '@core/models/user.model';
import { PostListComponent } from '@features/post/components/post-list/post-list.component';

@Component({
  selector: 'app-lessor-detail-tab',
  standalone: true,
  imports: [PostListComponent],
  templateUrl: './lessor-detail-tab.component.html',
  styleUrl: './lessor-detail-tab.component.scss',
})
export class LessorDetailTabComponent {
  @Input()
  lessor?: User;
}
