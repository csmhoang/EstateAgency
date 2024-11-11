import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatRadioModule } from '@angular/material/radio';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { Router, RouterModule } from '@angular/router';
import { provideNativeDateAdapter } from '@angular/material/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ToastService } from '@shared/services/toast/toast.service';
import { AuthService } from '@core/auth/services/auth.service';
import { MyValidators } from '@shared/validators/my-validators';
import { Register } from '@core/auth/models/register.model';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { catchError, of } from 'rxjs';
import { Login } from '@core/auth/models/login.model';

@Component({
  selector: 'app-register-form',
  standalone: true,
  providers: [provideNativeDateAdapter()],
  imports: [
    MatFormFieldModule,
    MatInputModule,
    RouterModule,
    MatIconModule,
    MatDatepickerModule,
    ReactiveFormsModule,
    MatRadioModule,
  ],
  templateUrl: './register-form.component.html',
  styleUrl: './register-form.component.scss',
})
export class RegisterFormComponent implements OnInit {
  destroyRef = inject(DestroyRef);
  router = inject(Router);
  maxDate = new Date(new Date().getFullYear() - 18, 0, 1);
  hidePassword = true;
  hideRepassword = true;

  form: FormGroup = new FormGroup({});
  email?: AbstractControl | null;
  fullName?: AbstractControl | null;
  phoneNumber?: AbstractControl | null;
  dateOfBirth?: AbstractControl | null;
  gender?: AbstractControl | null;
  address?: AbstractControl | null;
  password?: AbstractControl | null;
  repassword?: AbstractControl | null;
  roles?: string[];

  constructor(
    private formBuilder: FormBuilder,
    private toastService: ToastService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      email: this.formBuilder.control('', [
        Validators.required,
        Validators.email,
      ]),
      fullName: this.formBuilder.control('', [
        Validators.required,
        MyValidators.letter(),
      ]),
      phoneNumber: this.formBuilder.control('', [
        Validators.required,
        MyValidators.phone(),
      ]),
      dateOfBirth: this.formBuilder.control(''),
      gender: this.formBuilder.control('Male'),
      address: this.formBuilder.control('', [Validators.required]),
      password: this.formBuilder.control('', [
        Validators.required,
        Validators.minLength(6),
      ]),
      repassword: this.formBuilder.control('', [
        Validators.required,
        MyValidators.passwordMatch('password'),
      ]),
    });

    this.email = this.form.get('email');
    this.fullName = this.form.get('fullName');
    this.phoneNumber = this.form.get('phoneNumber');
    this.dateOfBirth = this.form.get('dateOfBirth');
    this.address = this.form.get('address');
    this.password = this.form.get('password');
    this.repassword = this.form.get('repassword');
  }

  onRegister() {
    if (this.form.valid) {
      const credentials: Register = {
        ...this.form.value,
        roles: this.roles,
      };
      this.authService
        .register(credentials)
        .pipe(
          takeUntilDestroyed(this.destroyRef),
          catchError(() => of(null))
        )
        .subscribe({
          next: (response) => {
            if (response?.success) {
              this.authService.account.set({
                email: credentials.email,
                password: credentials.password,
                isRemember: true,
              } as Login);
              this.authService
                .sendEmailConfirm(credentials.email)
                .pipe(
                  takeUntilDestroyed(this.destroyRef),
                  catchError(() => of(null))
                )
                .subscribe((response) => {
                  if (response?.success) {
                    this.router.navigate(['/verify-email']);
                  }
                });
              this.toastService.success(
                'Đăng ký thành công, vui lòng xác thực email!'
              );
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

  errorForAddress(): string {
    if (this.address?.hasError('required')) {
      return 'Địa chỉ cụ thể không được để trống!';
    }
    return '';
  }

  errorForFullName(): string {
    if (this.fullName?.hasError('required')) {
      return 'Họ và tên không được để trống!';
    }
    if (this.fullName?.hasError('letter')) {
      return 'Họ và tên không được chứa số và ký tự!';
    }
    return '';
  }

  errorForPhoneNumber(): string {
    if (this.phoneNumber?.hasError('required')) {
      return 'Số điện thoại không được để trống!';
    }
    if (this.phoneNumber?.hasError('phone')) {
      return 'Số điện thoại không hợp lệ!';
    }
    return '';
  }

  errorForPassword(): string {
    if (this.password?.hasError('required')) {
      return 'Mật khẩu không được để trống!';
    }
    if (this.password?.hasError('minlength')) {
      return 'Mật khẩu cần ít nhất 6 ký tự!';
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
