import { Component } from '@angular/core';
import { FooterComponent } from '@features/footer/footer.component';
import { HeaderComponent } from '@features/header/header.component';
import { LessorDetailProfileComponent } from '@features/lessor/components/lessor-detail-profile/lessor-detail-profile.component';
import { LessorDetailTabComponent } from '@features/lessor/components/lessor-detail-tab/lessor-detail-tab.component';
import { CommentComponent } from '@shared/components/comment/comment.component';
import { ScrollTopComponent } from '@shared/components/scroll-top/scroll-top.component';

@Component({
  selector: 'app-lessor-detail-page',
  standalone: true,
  imports: [
    HeaderComponent,
    FooterComponent,
    ScrollTopComponent,
    CommentComponent,
    LessorDetailProfileComponent,
    LessorDetailTabComponent,
  ],
  templateUrl: './lessor-detail-page.component.html',
  styleUrl: './lessor-detail-page.component.scss',
})
export class LessorDetailPageComponent {}
