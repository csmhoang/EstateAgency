import { Component } from '@angular/core';
import { FooterComponent } from '@features/footer/footer.component';
import { HeaderComponent } from '@features/header/header.component';
import { CommentComponent } from '@shared/components/comment/comment.component';
import { ScrollTopComponent } from '@shared/components/scroll-top/scroll-top.component';
import { LessorInfoCardComponent } from '@features/lessor/components/lessor-info-card/lessor-info-card.component';
import { ApartmentDetailTabComponent } from '@features/apartment/components/apartment-detail-tab/apartment-detail-tab.component';
import { ApartmentDetailPhotosComponent } from '@features/apartment/components/apartment-detail-photos/apartment-detail-photos.component';
import { ApartmentPrimaryInfoComponent } from '@features/apartment/components/apartment-primary-info/apartment-primary-info.component';
import { ApartmentRelationshipComponent } from '@features/apartment/components/apartment-relationship/apartment-relationship.component';

@Component({
  selector: 'app-apartment-detail-page',
  standalone: true,
  imports: [
    HeaderComponent,
    FooterComponent,
    ScrollTopComponent,
    CommentComponent,
    ApartmentDetailPhotosComponent,
    ApartmentDetailTabComponent,
    LessorInfoCardComponent,
    ApartmentPrimaryInfoComponent,
    ApartmentRelationshipComponent
],
  templateUrl: './apartment-detail-page.component.html',
  styleUrl: './apartment-detail-page.component.scss',
})
export class ApartmentDetailPageComponent {}
