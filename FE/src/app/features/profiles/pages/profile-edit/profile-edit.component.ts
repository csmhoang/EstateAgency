import { CommonModule } from '@angular/common';
import { Component, computed } from '@angular/core';
import { UserService } from '@core/services/user.service';
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
  user = computed(() => this.userService.currentUser());

  constructor(private userService: UserService) {}
}
