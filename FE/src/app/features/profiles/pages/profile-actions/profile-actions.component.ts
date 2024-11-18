import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { User } from '@core/models/user.model';

import { LessorListComponent } from '@features/lessor/components/lessor-list/lessor-list.component';
import { PostListComponent } from '@features/post/components/post-list/post-list.component';

@Component({
  selector: 'app-profile-actions',
  standalone: true,
  imports: [
    CommonModule,
    LessorListComponent,
    PostListComponent,
  ],
  templateUrl: './profile-actions.component.html',
  styleUrl: './profile-actions.component.scss',
})
export class ProfileActionsComponent {
  user: User = {
    id: '3243124324234',
    username: '',
    fullName: 'Cao Sỹ Minh Hoàng',
    phoneNumber: '0393212312',
    email: 'mhoang@gmail.com',
    address: ""
  };
}
