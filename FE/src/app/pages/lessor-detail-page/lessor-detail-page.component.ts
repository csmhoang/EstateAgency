import { Component } from '@angular/core';
import { LessorDetailProfileComponent } from '@features/lessor/components/lessor-detail-profile/lessor-detail-profile.component';
import { LessorDetailTabComponent } from '@features/lessor/components/lessor-detail-tab/lessor-detail-tab.component';
import { CommentComponent } from '@shared/components/comment/comment.component';

@Component({
  selector: 'app-lessor-detail-page',
  standalone: true,
  imports: [
    CommentComponent,
    LessorDetailProfileComponent,
    LessorDetailTabComponent,
  ],
  templateUrl: './lessor-detail-page.component.html',
  styleUrl: './lessor-detail-page.component.scss',
})
export class LessorDetailPageComponent {}
