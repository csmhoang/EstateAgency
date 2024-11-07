import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatStepperModule } from '@angular/material/stepper';
import { Router } from '@angular/router';
import { ResetPassword } from '@core/auth/models/reset-password.model';
import { AuthService } from '@core/auth/services/auth.service';
import { HeaderComponent } from '@core/layout/header/header.component';
import { ToastService } from '@shared/services/toast/toast.service';
import { MyValidators } from '@shared/validators/my-validators';
import { catchError, of } from 'rxjs';

@Component({
  selector: 'app-verify-reset-password',
  standalone: true,
  imports: [
    HeaderComponent,
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    MatStepperModule,
    MatIconModule,
  ],
  templateUrl: './verify-reset-password.component.html',
  styleUrl: './verify-reset-password.component.scss',
})
export class VerifyResetPasswordComponent implements OnInit {
  destroyRef = inject(DestroyRef);
  router = inject(Router);
  hideNewPassword = true;
  hideRepassword = true;

  firtForm: FormGroup = new FormGroup({});
  secondForm: FormGroup = new FormGroup({});
  email?: AbstractControl | null;
  token?: AbstractControl | null;
  newPassword?: AbstractControl | null;
  repassword?: AbstractControl | null;

  constructor(
    private formBuilder: FormBuilder,
    private toastService: ToastService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.firtForm = this.formBuilder.group({
      email: this.formBuilder.control('', [
        Validators.required,
        Validators.email,
      ]),
    });
    this.email = this.firtForm.get('email');

    this.secondForm = this.formBuilder.group({
      token: this.formBuilder.control('', [Validators.required]),
      newPassword: this.formBuilder.control('', [
        Validators.required,
        Validators.minLength(6),
      ]),
      repassword: this.formBuilder.control('', [
        Validators.required,
        MyValidators.passwordMatch('newPassword'),
      ]),
    });

    this.token = this.secondForm.get('token');
    this.newPassword = this.secondForm.get('newPassword');
    this.repassword = this.secondForm.get('repassword');
  }

  onSendForgotPassword() {
    if (this.firtForm.valid) {
      const email: string = this.email?.value;
      this.authService
        .sendForgotPassword(email)
        .pipe(
          takeUntilDestroyed(this.destroyRef),
          catchError(() => of(null))
        )
        .subscribe({
          next: (response) => {
            if (response?.success) {
              this.toastService.success('Vui lòng kiểm tra email!');
            }
          },
        });
    }
  }

  onResetPassword() {
    if (this.secondForm.valid) {
      const credentials: ResetPassword = {
        email: this.email?.value,
        newPassword: this.newPassword?.value,
        token: this.token?.value,
      };
      this.authService
        .resetPassword(credentials)
        .pipe(
          takeUntilDestroyed(this.destroyRef),
          catchError(() => of(null))
        )
        .subscribe({
          next: (response) => {
            if (response?.success) {
              this.toastService.success('Đặt lại mật khẩu thành công!');
              this.router.navigate(['/login']);
            }
          },
        });
    }
  }

  errorForEmail(): string {
    if (this.email?.hasError('required')) {
      return 'Email không được để trống!';
    }
    if (this.email?.hasError('email')) {
      return 'Email không hợp lệ!';
    }
    return '';
  }

  errorForToken(): string {
    if (this.token?.hasError('required')) {
      return 'Token không được để trống!';
    }
    return '';
  }

  errorForNewPassword(): string {
    if (this.newPassword?.hasError('required')) {
      return 'Mật khẩu mới không được để trống!';
    }
    if (this.newPassword?.hasError('minlength')) {
      return 'Mật khẩu mới cần ít nhất 6 ký tự!';
    }
    return '';
  }

  errorForRepassword(): string {
    if (this.repassword?.hasError('required')) {
      return 'Vui lòng nhập lại mật khẩu!';
    }
    if (this.repassword?.hasError('passwordMatch')) {
      return 'Mật khẩu không khớp. Vui lòng kiểm tra lại!';
    }
    return '';
  }
}
