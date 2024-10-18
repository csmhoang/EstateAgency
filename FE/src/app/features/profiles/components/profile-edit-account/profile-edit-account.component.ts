import { Component, Input } from '@angular/core';
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
import { User } from '@core/models/user.model';
import { ToastService } from '@shared/services/toast/toast.service';
import { MyValidators } from '@shared/validators/my-validators';

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
  @Input({ required: true })
  user?: User;

  hideOldPassword = true;
  hideNewPassword = true;
  hideRepassword = true;

  form: FormGroup = new FormGroup({});
  oldPassword?: AbstractControl | null;
  newPassword?: AbstractControl | null;
  repassword?: AbstractControl | null;

  constructor(
    private formBuilder: FormBuilder,
    private toastService: ToastService
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
    // if (this.form.valid) {
    //   const credentials: Register = {
    //     ...this.form.value,
    //     roles: this.roles,
    //   };
    //   this.authService
    //     .register(credentials)
    //     .pipe(takeUntilDestroyed(this.destroyRef))
    //     .subscribe({
    //       next: (response) => {
    //         if (response.success) {
    //           this.authService
    //             .login(
    //               {
    //                 email: credentials.email,
    //                 password: credentials.password,
    //                 isRemember: true,
    //               } as Login,
    //               true
    //             )
    //             .pipe(take(1))
    //             .subscribe(() => void this.router.navigate(['/']));
    //         }
    //       },
    //       error: () => {
    //         this.toastService.error('Đăng ký thất bại, vui lòng thử lại!');
    //       },
    //     });
    // }
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
