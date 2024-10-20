import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { User } from '@core/models/user.model';
import { ProfileEditAccountComponent } from '@features/profiles/components/profile-edit-account/profile-edit-account.component';
import { ProfileEditOverviewComponent } from '@features/profiles/components/profile-edit-overview/profile-edit-overview.component';
import { ProfileEditProfileComponent } from '@features/profiles/components/profile-edit-profile/profile-edit-profile.component';


@Component({
  selector: 'app-profile-edit',
  standalone: true,
  imports: [
    CommonModule,
    ProfileEditOverviewComponent,
    ProfileEditProfileComponent,
    ProfileEditAccountComponent,
  ],
  templateUrl: './profile-edit.component.html',
  styleUrl: './profile-edit.component.scss',
})
export class ProfileEditComponent {
  user: User = {
    id: '3243124324234',
    userCode: 1,
    fullName: 'Cao Sỹ Minh Hoàng',
    phoneNumber: '0393212312',
    email: 'mhoang@gmail.com',
  };
}
