import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { Gender, User } from '@core/models/user.model';

@Component({
  selector: 'app-profile-edit-overview',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './profile-edit-overview.component.html',
  styleUrl: './profile-edit-overview.component.scss',
})
export class ProfileEditOverviewComponent {
  @Input({ required: true })
  user?: User;
  gender = Gender[this.user?.gender || ''];
}
