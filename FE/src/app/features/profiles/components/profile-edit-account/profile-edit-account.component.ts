import { Component, DestroyRef, inject, Input } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { ChangePassword } from '@core/auth/models/changePassword.model';
import { AuthService } from '@core/auth/services/auth.service';
import { User } from '@core/models/user.model';
import { ToastService } from '@shared/services/toast/toast.service';
import { MyValidators } from '@shared/validators/my-validators';
import { catchError, of } from 'rxjs';

@Component({
  selector: 'app-profile-edit-account',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    ReactiveFormsModule,
  ],
  templateUrl: './profile-edit-account.component.html',
  styleUrl: './profile-edit-account.component.scss',
})
export class ProfileEditAccountComponent {
  destroyRef = inject(DestroyRef);
  @Input()
  user?: User | null;

  hideOldPassword = true;
  hideNewPassword = true;
  hideRepassword = true;

  form: FormGroup = new FormGroup({});
  oldPassword?: AbstractControl | null;
  newPassword?: AbstractControl | null;
  repassword?: AbstractControl | null;

  constructor(
    private formBuilder: FormBuilder,
    private toastService: ToastService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      oldPassword: this.formBuilder.control('', [
        Validators.required,
        Validators.minLength(6),
      ]),
      newPassword: this.formBuilder.control('', [
        Validators.required,
        Validators.minLength(6),
      ]),
      repassword: this.formBuilder.control('', [
        Validators.required,
        MyValidators.passwordMatch('newPassword'),
      ]),
    });

    this.oldPassword = this.form.get('oldPassword');
    this.newPassword = this.form.get('newPassword');
    this.repassword = this.form.get('repassword');
  }

  onChangePassword() {
    if (this.form.valid && this.user) {
      const credentials: ChangePassword = {
        email: this.user.email,
        password: this.oldPassword?.value,
        newPassword: this.newPassword?.value,
      };
      this.authService
        .changePassWord(credentials)
        .pipe(
          takeUntilDestroyed(this.destroyRef),
          catchError(() => of(null))
        )
        .subscribe({
          next: (response) => {
            if (response?.success) {
              this.toastService.success('Đổi mật khẩu thành công!');
            }
          },
        });
    }
  }
  errorForOldPassword(): string {
    if (this.oldPassword?.hasError('required')) {
      return 'Mật khẩu cũ không được để trống!';
    }
    if (this.oldPassword?.hasError('minlength')) {
      return 'Mật khẩu cũ cần ít nhất 6 ký tự!';
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
