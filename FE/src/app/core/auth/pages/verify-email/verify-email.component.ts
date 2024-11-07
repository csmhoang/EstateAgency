import { CommonModule } from '@angular/common';
import { Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatStepperModule } from '@angular/material/stepper';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { AuthService } from '@core/auth/services/auth.service';
import { HeaderComponent } from '@core/layout/header/header.component';
import { ToastService } from '@shared/services/toast/toast.service';
import { catchError, of, take } from 'rxjs';

@Component({
  selector: 'app-verify-email',
  standalone: true,
  imports: [
    HeaderComponent,
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
  ],
  templateUrl: './verify-email.component.html',
  styleUrl: './verify-email.component.scss',
})
export class VerifyEmailComponent implements OnInit {
  destroyRef = inject(DestroyRef);
  router = inject(Router);

  form: FormGroup = new FormGroup({});
  token?: AbstractControl | null;
  account = this.authService.account();

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private toastService: ToastService
  ) {}

  ngOnInit() {
    this.form = this.formBuilder.group({
      token: this.formBuilder.control('', [Validators.required]),
    });
    this.token = this.form.get('token');
  }

  onConfirmEmail() {
    if (this.form.valid && this.account) {
      this.authService
        .emailConfirm(this.account.email, this.token?.value)
        .pipe(
          takeUntilDestroyed(this.destroyRef),
          catchError(() => of(null))
        )
        .subscribe((response) => {
          if (response?.success) {
            this.toastService.success('Xác thực Email thành công!');
            this.authService
              .login(this.account!)
              .pipe(
                take(1),
                catchError(() => of(null))
              )
              .subscribe();
          } else {
            this.toastService.error('Xác thực Email thất bại!');
          }
        });
    }
  }

  errorForToken(): string {
    if (this.token?.hasError('required')) {
      return 'Token không được để trống!';
    }
    return '';
  }
}
