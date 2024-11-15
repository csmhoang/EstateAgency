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
import { MatSelectModule } from '@angular/material/select';
import { PostService } from '@features/post/services/post.service';
import { ToastService } from '@shared/services/toast/toast.service';
import { Router, RouterLink } from '@angular/router';
import { Post } from '@features/post/models/post.model';
import { catchError, lastValueFrom, map, of } from 'rxjs';
import { CommonModule } from '@angular/common';
import { UserService } from '@core/services/user.service';
@Component({
  selector: 'app-post-insert',
  standalone: true,
  providers: [provideNativeDateAdapter()],
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    ReactiveFormsModule,
    MatSelectModule,
    CommonModule,
    RouterLink,
  ],
  templateUrl: './post-insert.component.html',
  styleUrl: './post-insert.component.scss',
})
export class PostInsertComponent implements OnInit {
  destroyRef = inject(DestroyRef);
  minDate = new Date();
  user = this.userService.currentUser();
  room$ = lastValueFrom(
    this.postService.getRooms().pipe(
      map((data) => data.filter((r) => r.condition === 'Available')),
      takeUntilDestroyed(this.destroyRef),
      catchError(() => of(null))
    )
  );

  form: FormGroup = new FormGroup({});

  title?: AbstractControl | null;
  room?: AbstractControl | null;
  description?: AbstractControl | null;
  availableFrom?: AbstractControl | null;

  constructor(
    private formBuilder: FormBuilder,
    private toastService: ToastService,
    private userService: UserService,
    private router: Router,
    private postService: PostService
  ) {}

  ngOnInit() {
    this.form = this.formBuilder.group({
      title: this.formBuilder.control('', [Validators.required]),
      room: this.formBuilder.control('', [Validators.required]),
      description: this.formBuilder.control(''),
      availableFrom: this.formBuilder.control(new Date()),
    });

    this.title = this.form.get('title');
    this.room = this.form.get('room');
    this.description = this.form.get('description');
    this.availableFrom = this.form.get('availableFrom');
  }

  onInsert() {
    if (this.form.valid) {
      const post: Post = {
        ...this.form.value,
        room: null,
        roomId: this.room?.value.id,
        landlordId: this.user?.id,
      };

      this.postService
        .insert(post)
        .pipe(
          takeUntilDestroyed(this.destroyRef),
          catchError(() => of(null))
        )
        .subscribe({
          next: (response) => {
            if (response?.success) {
              this.toastService.success('Thêm bài thành công');
              void this.router.navigate(['/lessor/post']);
            }
          },
        });
    }
  }

  errorForTitle(): string {
    if (this.title?.hasError('required')) {
      return 'Tiêu đề bài đăng không được để trống!';
    }
    return '';
  }

  errorForRoom(): string {
    if (this.room?.hasError('required')) {
      return 'Vui lòng chọn phòng!';
    }
    return '';
  }
}
