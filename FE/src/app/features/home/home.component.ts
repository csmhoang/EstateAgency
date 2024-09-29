import { Component } from '@angular/core';
import { HeaderComponent } from '../header/header.component';
import { NgbNavModule } from '@ng-bootstrap/ng-bootstrap';
import { FooterComponent } from '../footer/footer.component';
import { ListPostComponent } from '../list-post/list-post.component';
import { AutoCompleteComponent } from '../../shared/components/app_inputs/auto-complete/auto-complete.component';
import { ListFeedbackComponent } from '../list-feedback/list-feedback.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    HeaderComponent,
    NgbNavModule,
    AutoCompleteComponent,
    FooterComponent,
    ListFeedbackComponent,
    ListPostComponent,
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent {
  posts: any;
}
