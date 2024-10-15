import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { RouterModule } from '@angular/router';
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
  ],
  templateUrl: './register-form.component.html',
  styleUrl: './register-form.component.scss',
})
export class RegisterFormComponent implements OnInit {
  destroyRef = inject(DestroyRef);
  hidePassword = true;
  hideRepassword = true;
  form: FormGroup = new FormGroup({});

  email?: AbstractControl | null;
  lastname?: AbstractControl | null;
  firstname?: AbstractControl | null;
  phone?: AbstractControl | null;
  birthday?: AbstractControl | null;
  address?: AbstractControl | null;
  password?: AbstractControl | null;
  repassword?: AbstractControl | null;

  constructor(
    private formBuilder: FormBuilder,
    private toastService: ToastService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.form = this.formBuilder.group(
      {
        email: this.formBuilder.control('', [
          Validators.required,
          Validators.email,
        ]),
        lastname: this.formBuilder.control('', [
          Validators.required,
          MyValidators.letter(),
        ]),
        firstname: this.formBuilder.control('', [
          Validators.required,
          MyValidators.letter(),
        ]),
        phone: this.formBuilder.control('', [
          Validators.required,
          MyValidators.phone(),
        ]),
        birthday: this.formBuilder.control(''),
        address: this.formBuilder.control(''),
        password: this.formBuilder.control('', [
          Validators.required,
          Validators.minLength(6),
        ]),
        repassword: this.formBuilder.control('', [Validators.required]),
      },
      { validators: MyValidators.passwordMatch() }
    );

    this.email = this.form.get('email');
    this.lastname = this.form.get('lastname');
    this.firstname = this.form.get('firstname');
    this.phone = this.form.get('phone');
    this.birthday = this.form.get('birthday');
    this.address = this.form.get('address');
    this.password = this.form.get('password');
    this.repassword = this.form.get('repassword');
  }

  onRegister() {
    if (this.form.valid) {
      const credentials: any = this.form.value;
      console.log(credentials);
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

  errorForLastname(): string {
    if (this.lastname?.hasError('required')) {
      return 'Họ không được để trống!';
    }
    if (this.lastname?.hasError('letter')) {
      return 'Họ không được chứa số và ký tự!';
    }
    return '';
  }

  errorForFirstname(): string {
    if (this.firstname?.hasError('required')) {
      return 'Tên không được để trống!';
    }
    if (this.firstname?.hasError('letter')) {
      return 'Tên không được chứa số và ký tự!';
    }
    return '';
  }

  errorForPhone(): string {
    if (this.phone?.hasError('required')) {
      return 'Số điện thoại không được để trống!';
    }
    if (this.phone?.hasError('phone')) {
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
    if (this.repassword?.hasError('repassword')) {
      return 'Mật khẩu không khớp. Vui lòng kiểm tra lại!';
    }
    return '';
  }
}
