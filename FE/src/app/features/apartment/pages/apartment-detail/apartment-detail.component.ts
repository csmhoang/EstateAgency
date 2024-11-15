import { CommonModule } from '@angular/common';
import {
  Component,
  computed,
  DestroyRef,
  inject,
  OnDestroy,
  OnInit,
} from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { FooterComponent } from '@core/layout/footer/footer.component';
import { HeaderComponent } from '@core/layout/header/header.component';
import { User } from '@core/models/user.model';
import { UserService } from '@core/services/user.service';
import { ApartmentDetailPhotosComponent } from '@features/apartment/components/apartment-detail-photos/apartment-detail-photos.component';
import { ApartmentDetailTabComponent } from '@features/apartment/components/apartment-detail-tab/apartment-detail-tab.component';
import { ApartmentPrimaryInfoComponent } from '@features/apartment/components/apartment-primary-info/apartment-primary-info.component';
import { ApartmentRelationshipComponent } from '@features/apartment/components/apartment-relationship/apartment-relationship.component';
import { FeedbackItemComponent } from '@features/apartment/components/feedback-item/feedback-item.component';
import { Feedback } from '@features/apartment/models/feedback.model';
import { FeedbackService } from '@features/apartment/services/feedback.service';
import { LessorInfoCardComponent } from '@features/lessor/components/lessor-info-card/lessor-info-card.component';
import { Post } from '@features/post/models/post.model';
import { NgbCollapseModule, NgbRatingModule } from '@ng-bootstrap/ng-bootstrap';
import { CommentComponent } from '@shared/components/comment/comment.component';

@Component({
  selector: 'app-apartment-detail',
  standalone: true,
  imports: [
    HeaderComponent,
    FooterComponent,
    ApartmentDetailPhotosComponent,
    ApartmentDetailTabComponent,
    LessorInfoCardComponent,
    ApartmentPrimaryInfoComponent,
    ApartmentRelationshipComponent,
    RouterLink,
    ReactiveFormsModule,
    NgbRatingModule,
    CommonModule,
    FeedbackItemComponent,
  ],
  templateUrl: './apartment-detail.component.html',
  styleUrl: './apartment-detail.component.scss',
})
export class ApartmentDetailComponent implements OnInit, OnDestroy {
  post?: Post | null;
  user = this.userService.currentUser();
  feedbacks = computed(() => this.feedbackService.feedbackThread());

  form: FormGroup = new FormGroup({});

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private feedbackService: FeedbackService,
    private userService: UserService
  ) {}

  ngOnInit() {
    this.form = this.formBuilder.group({
      rating: this.formBuilder.control(0),
      comment: this.formBuilder.control('', [Validators.required]),
    });

    this.post = this.route.snapshot.data['post'];
    if (this.post) {
      this.feedbackService.createHubConnection(this.post.id);
    } else {
      this.feedbackService.stopHubConnection();
    }
  }

  async sendFeedback() {
    if (this.form.valid) {
      const feedback: Feedback = {
        ...this.form.value,
        tenantId: this.user?.id,
        postId: this.post?.id,
      };
      await this.feedbackService.sendFeedback(feedback);
      this.form.reset();
    }
  }

  async onReply(feedback: Feedback) {
    if (feedback) {
      await this.feedbackService.sendFeedback({
        ...feedback,
        tenantId: this.user?.id,
        postId: this.post?.id,
      });
    }
  }

  ngOnDestroy() {
    this.feedbackService.stopHubConnection();
  }
}
