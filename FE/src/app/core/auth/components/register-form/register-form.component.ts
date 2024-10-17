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
import { take } from 'rxjs';
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
  hidePassword = true;
  hideRepassword = true;
  title?: string;
  control: string = '';

  form: FormGroup = new FormGroup({});
  email?: AbstractControl | null;
  fullname?: AbstractControl | null;
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
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    const url = this.router.url;
    if (url.includes('/lessor/register')) {
      this.title = 'người cho thuê';
      this.roles = ['Landlord'];
      this.control = '/lessor';
    } else if (url.includes('/admin/register')) {
      this.title = 'người quản trị';
      this.roles = ['Admin'];
      this.control = '/admin';
    } else {
      this.title = 'người thuê';
      this.roles = ['Tenant'];
    }
    this.form = this.formBuilder.group({
      email: this.formBuilder.control('', [
        Validators.required,
        Validators.email,
      ]),
      fullname: this.formBuilder.control('', [
        Validators.required,
        MyValidators.letter(),
      ]),
      phoneNumber: this.formBuilder.control('', [
        Validators.required,
        MyValidators.phone(),
      ]),
      dateOfBirth: this.formBuilder.control(''),
      gender: this.formBuilder.control('0'),
      address: this.formBuilder.control(''),
      password: this.formBuilder.control('', [
        Validators.required,
        Validators.minLength(6),
      ]),
      repassword: this.formBuilder.control('', [
        Validators.required,
        MyValidators.passwordMatch(),
      ]),
    });

    this.email = this.form.get('email');
    this.fullname = this.form.get('fullname');
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
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe({
          next: (response) => {
            if (response.success) {
              this.authService
                .login(
                  {
                    email: credentials.email,
                    password: credentials.password,
                    isRemember: true,
                  } as Login,
                  true
                )
                .pipe(take(1))
                .subscribe(() => void this.router.navigate(['/']));
            }
          },
          error: () => {
            this.toastService.error('Đăng ký thất bại, vui lòng thử lại!');
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

  errorForFullname(): string {
    if (this.fullname?.hasError('required')) {
      return 'Họ và tên không được để trống!';
    }
    if (this.fullname?.hasError('letter')) {
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
