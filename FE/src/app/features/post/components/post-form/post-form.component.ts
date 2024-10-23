import { CommonModule } from '@angular/common';
import { Component, DestroyRef, inject, OnInit } from '@angular/core';
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
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import { User } from '@core/models/user.model';
import { UserService } from '@core/services/user.service';
import { Place } from '@features/post/models/place.model';
import { Room } from '@features/apartment/models/room.model';
import { PostService } from '@features/post/services/post.service';
import { ToastService } from '@shared/services/toast/toast.service';
import { FileUploader, FileUploadModule } from 'ng2-file-upload';
import { firstValueFrom } from 'rxjs';
import { RouterLink } from '@angular/router';
import { Post } from '@features/post/models/post.model';

@Component({
  selector: 'app-post-form',
  standalone: true,
  providers: [provideNativeDateAdapter()],
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    ReactiveFormsModule,
    MatSelectModule,
    RouterLink,
  ],
  templateUrl: './post-form.component.html',
  styleUrl: './post-form.component.scss',
})
export class PostFormComponent implements OnInit {
  destroyRef = inject(DestroyRef);
  minDate = new Date();

  form: FormGroup = new FormGroup({});
  title?: AbstractControl | null;
  roomId?: AbstractControl | null;
  description?: AbstractControl | null;
  availableFrom?: AbstractControl | null;

  constructor(
    private formBuilder: FormBuilder,
    private toastService: ToastService,
    public postService: PostService
  ) {}

  ngOnInit() {
    this.form = this.formBuilder.group({
      title: this.formBuilder.control('', [Validators.required]),
      roomId: this.formBuilder.control('', [Validators.required]),
      description: this.formBuilder.control(''),
      availableFrom: this.formBuilder.control(new Date()),
    });

    this.title = this.form.get('title');
    this.roomId = this.form.get('roomId');
    this.description = this.form.get('description');
    this.availableFrom = this.form.get('availableFrom');
  }

  onInsert() {
    if (this.form.valid) {
      const post: Post = this.form.value;
      this.postService
        .insert(post)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe({
          next: (response) => {
            if (response.success) {
              this.toastService.success(response.messages);
            }
          },
        });
    }
  }

  errorForTitle(): string {
    if (this.title?.hasError('required')) {
      return 'Tiêu đề không được để trống!';
    }
    return '';
  }

  errorForRoomId(): string {
    if (this.roomId?.hasError('required')) {
      return 'Vui lòng chọn phòng!';
    }
    return '';
  }
}
