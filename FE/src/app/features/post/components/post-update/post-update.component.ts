import { Component, DestroyRef, inject, Input, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { provideNativeDateAdapter } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { Post } from '@features/post/models/post.model';
import { PostService } from '@features/post/services/post.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { catchError, of } from 'rxjs';

@Component({
  selector: 'app-post-update',
  standalone: true,
  providers: [provideNativeDateAdapter()],
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    ReactiveFormsModule,
    MatSelectModule,
  ],
  templateUrl: './post-update.component.html',
  styleUrl: './post-update.component.scss',
})
export class PostUpdateComponent implements OnInit {
  @Input() data!: Post;

  destroyRef = inject(DestroyRef);
  activeModal = inject(NgbActiveModal);

  form: FormGroup = new FormGroup({});

  title?: AbstractControl | null;
  description?: AbstractControl | null;
  availableFrom?: AbstractControl | null;

  constructor(
    private formBuilder: FormBuilder,
    private postService: PostService
  ) {}

  ngOnInit() {
    this.form = this.formBuilder.group({
      title: this.formBuilder.control(this.data.title, [Validators.required]),
      description: this.formBuilder.control(this.data.description),
      availableFrom: this.formBuilder.control(this.data.availableFrom),
    });

    this.title = this.form.get('title');
    this.description = this.form.get('description');
    this.availableFrom = this.form.get('availableFrom');
  }

  onUpdate() {
    if (this.form.valid) {
      const post: Post = {
        ...this.form.value,
      };
      this.postService
        .update(this.data.id, post)
        .pipe(
          takeUntilDestroyed(this.destroyRef),
          catchError(() => of(null))
        )
        .subscribe((response) => {
          if (response?.success) {
            this.activeModal.close(true);
          }
        });
    }
  }

  accept() {
    this.onUpdate();
  }

  decline() {
    this.activeModal.dismiss(false);
  }

  errorForTitle(): string {
    if (this.title?.hasError('required')) {
      return 'Tiêu đề bài đăng không được để trống!';
    }
    return '';
  }
}
