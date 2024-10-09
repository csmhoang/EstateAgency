import { Component } from '@angular/core';
import { FooterComponent } from '@core/layout/footer/footer.component';
import { HeaderComponent } from '@core/layout/header/header.component';
import { LessorDetailProfileComponent } from '@features/lessor/components/lessor-detail-profile/lessor-detail-profile.component';
import { LessorDetailTabComponent } from '@features/lessor/components/lessor-detail-tab/lessor-detail-tab.component';
import { CommentComponent } from '@shared/components/comment/comment.component';

@Component({
  selector: 'app-lessor-detail',
  standalone: true,
  imports: [
    HeaderComponent,
    FooterComponent,
    CommentComponent,
    LessorDetailProfileComponent,
    LessorDetailTabComponent,
  ],
  templateUrl: './lessor-detail.component.html',
  styleUrl: './lessor-detail.component.scss',
})
export class LessorDetailComponent {}
