import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { Router, RouterModule } from '@angular/router';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Login } from '@core/auth/models/login.model';
import { ToastService } from '@shared/services/toast/toast.service';
import { AuthService } from '@core/auth/services/auth.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-login-form',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatInputModule,
    RouterModule,
    MatIconModule,
    ReactiveFormsModule,
  ],
  templateUrl: './login-form.component.html',
  styleUrl: './login-form.component.scss',
})
export class LoginFormComponent implements OnInit {
  destroyRef = inject(DestroyRef);
  hidePass = true;
  title?: string;
  control: string = '';

  form: FormGroup = new FormGroup({});
  email?: AbstractControl | null;
  password?: AbstractControl | null;
  isRemember?: AbstractControl | null;

  constructor(
    private formBuilder: FormBuilder,
    private toastService: ToastService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    const url = this.router.url;
    if (url.includes('/lessor/login')) {
      this.title = 'người cho thuê';
      this.control = '/lessor';
    } else if (url.includes('/admin/login')) {
      this.title = 'người quản trị';
      this.control = '/admin';
    } else {
      this.title = 'người thuê';
    }
    this.form = this.formBuilder.group({
      email: this.formBuilder.control('', [
        Validators.required,
        Validators.email,
      ]),
      password: this.formBuilder.control('', [
        Validators.required,
        Validators.minLength(6),
      ]),
      isRemember: this.formBuilder.control(true),
    });

    this.email = this.form.get('email');
    this.password = this.form.get('password');
    this.isRemember = this.form.get('isRemember');
  }

  async onLogin() {
    if (this.form.valid) {
      const credentials: Login = this.form.value;
      this.authService
        .login(credentials)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe({
          next: () => void this.router.navigate(['/']),
          error: () => {
            this.toastService.error('Đăng nhập thất bại, vui lòng thử lại!');
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

  errorForPassword(): string {
    if (this.password?.hasError('required')) {
      return 'Mật khẩu không được để trống!';
    }
    if (this.password?.hasError('minlength')) {
      return 'Mật khẩu cần ít nhất 6 ký tự!';
    }
    return '';
  }
}