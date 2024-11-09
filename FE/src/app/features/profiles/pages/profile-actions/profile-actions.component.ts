import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { User } from '@core/models/user.model';

import { LessorListComponent } from '@features/lessor/components/lessor-list/lessor-list.component';
import { PostListComponent } from '@features/post/components/post-list/post-list.component';
import { ProfileEditAccountComponent } from '@features/profiles/components/profile-edit-account/profile-edit-account.component';
import { ProfileEditOverviewComponent } from '@features/profiles/components/profile-edit-overview/profile-edit-overview.component';
import { ProfileEditProfileComponent } from '@features/profiles/components/profile-edit-profile/profile-edit-profile.component';

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
    fullName: 'Cao Sỹ Minh Hoàng',
    phoneNumber: '0393212312',
    email: 'mhoang@gmail.com',
  };
}
