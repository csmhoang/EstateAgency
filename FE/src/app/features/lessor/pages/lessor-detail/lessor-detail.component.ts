import { Component } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { FooterComponent } from '@core/layout/footer/footer.component';
import { HeaderComponent } from '@core/layout/header/header.component';
import { User } from '@core/models/user.model';
import { LessorDetailProfileComponent } from '@features/lessor/components/lessor-detail-profile/lessor-detail-profile.component';
import { LessorDetailTabComponent } from '@features/lessor/components/lessor-detail-tab/lessor-detail-tab.component';

@Component({
  selector: 'app-lessor-detail',
  standalone: true,
  imports: [
    HeaderComponent,
    FooterComponent,
    LessorDetailProfileComponent,
    LessorDetailTabComponent,
    RouterLink
  ],
  templateUrl: './lessor-detail.component.html',
  styleUrl: './lessor-detail.component.scss',
})
export class LessorDetailComponent {
  lessor: User = this.route.snapshot.data['lessor'];

  constructor(private route: ActivatedRoute) {}
}
