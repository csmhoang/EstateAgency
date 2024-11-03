import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { User } from '@core/models/user.model';
import { ProfileEditAccountComponent } from '@features/profiles/components/profile-edit-account/profile-edit-account.component';
import { ProfileEditOverviewComponent } from '@features/profiles/components/profile-edit-overview/profile-edit-overview.component';
import { ProfileEditProfileComponent } from '@features/profiles/components/profile-edit-profile/profile-edit-profile.component';
import { ProfileService } from '@features/profiles/services/profile.service';

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
  user = this.profileService.user;

  constructor(private profileService: ProfileService) {}
}
