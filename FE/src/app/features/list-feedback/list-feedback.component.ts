import { Component, Input } from '@angular/core';
import { FeedbackComponent } from '../feedback/feedback.component';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-list-feedback',
  standalone: true,
  imports: [RouterLink, FeedbackComponent],
  templateUrl: './list-feedback.component.html',
  styleUrl: './list-feedback.component.scss'
})
export class ListFeedbackComponent {
  @Input()
  items: any;

  ngOnInit(): void {
    this.items = ['', '', ''];
  }
}
