import { Component, Input, OnInit, output } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Feedback } from '@features/apartment/models/feedback.model';
import { NgbCollapseModule, NgbRatingModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-feedback-item',
  standalone: true,
  imports: [NgbCollapseModule, NgbRatingModule, ReactiveFormsModule],
  templateUrl: './feedback-item.component.html',
  styleUrl: './feedback-item.component.scss',
})
export class FeedbackItemComponent implements OnInit {
  @Input()
  feedback!: Feedback;
  reply = output<Feedback>();
  isCollapsed = true;
  form: FormGroup = new FormGroup({});

  constructor(private formBuilder: FormBuilder) {}

  ngOnInit() {
    this.form = this.formBuilder.group({
      comment: this.formBuilder.control('', [Validators.required]),
    });
  }

  timeSinceUpdateFilter(time: Date) {
    const now = new Date();
    const seconds = Math.floor(
      (now.getTime() - new Date(time).getTime()) / 1000
    );

    const intervals: { [key: string]: number } = {
      năm: 31536000,
      tháng: 2592000,
      tuần: 604800,
      ngày: 86400,
      giờ: 3600,
      phút: 60,
      giây: 1,
    };

    for (const [unit, value] of Object.entries(intervals)) {
      const interval = Math.floor(seconds / value);
      if (interval >= 1) {
        return `${interval} ${unit} trước`;
      }
    }

    return 'Vừa xong';
  }

  onReply(id: string) {
    if (this.form.valid) {
      const reply: Feedback = {
        ...this.form.value,
        replyId: id,
      };
      this.reply.emit(reply);
      this.form.reset();
    }
  }
}
