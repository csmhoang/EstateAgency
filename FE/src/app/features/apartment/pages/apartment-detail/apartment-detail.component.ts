import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { FooterComponent } from '@core/layout/footer/footer.component';
import { HeaderComponent } from '@core/layout/header/header.component';
import { ApartmentDetailPhotosComponent } from '@features/apartment/components/apartment-detail-photos/apartment-detail-photos.component';
import { ApartmentDetailTabComponent } from '@features/apartment/components/apartment-detail-tab/apartment-detail-tab.component';
import { ApartmentPrimaryInfoComponent } from '@features/apartment/components/apartment-primary-info/apartment-primary-info.component';
import { ApartmentRelationshipComponent } from '@features/apartment/components/apartment-relationship/apartment-relationship.component';
import { LessorInfoCardComponent } from '@features/lessor/components/lessor-info-card/lessor-info-card.component';
import { Post } from '@features/post/models/post.model';
import { CommentComponent } from '@shared/components/comment/comment.component';

@Component({
  selector: 'app-apartment-detail',
  standalone: true,
  imports: [
    HeaderComponent,
    FooterComponent,
    CommentComponent,
    ApartmentDetailPhotosComponent,
    ApartmentDetailTabComponent,
    LessorInfoCardComponent,
    ApartmentPrimaryInfoComponent,
    ApartmentRelationshipComponent,
    RouterLink,
  ],
  templateUrl: './apartment-detail.component.html',
  styleUrl: './apartment-detail.component.scss',
})
export class ApartmentDetailComponent {
  post: Post = this.route.snapshot.data['post'];

  constructor(private route: ActivatedRoute) {}
}
