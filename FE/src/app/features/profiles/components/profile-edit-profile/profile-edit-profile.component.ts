import { Component, DestroyRef, inject, Input, OnInit } from '@angular/core';
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
import { User } from '@core/models/user.model';
import { UserService } from '@core/services/user.service';
import { ToastService } from '@shared/services/toast/toast.service';
import { MyValidators } from '@shared/validators/my-validators';
import { catchError, of } from 'rxjs';

@Component({
  selector: 'app-profile-edit-profile',
  standalone: true,
  providers: [provideNativeDateAdapter()],
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    ReactiveFormsModule,
    MatRadioModule,
  ],
  templateUrl: './profile-edit-profile.component.html',
  styleUrl: './profile-edit-profile.component.scss',
})
export class ProfileEditProfileComponent implements OnInit {
  @Input()
  user?: User | null;

  destroyRef = inject(DestroyRef);
  maxDate = new Date(new Date().getFullYear() - 18, 0, 1);
  form: FormGroup = new FormGroup({});
  fullname?: AbstractControl | null;
  phoneNumber?: AbstractControl | null;
  dateOfBirth?: AbstractControl | null;
  gender?: AbstractControl | null;
  address?: AbstractControl | null;
  description?: AbstractControl | null;

  constructor(
    private formBuilder: FormBuilder,
    private toastService: ToastService,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      fullname: this.formBuilder.control(this.user?.fullName, [
        Validators.required,
        MyValidators.letter(),
      ]),
      phoneNumber: this.formBuilder.control(this.user?.phoneNumber, [
        Validators.required,
        MyValidators.phone(),
      ]),
      dateOfBirth: this.formBuilder.control(this.user?.dateOfBirth),
      gender: this.formBuilder.control(this.user?.gender),
      address: this.formBuilder.control(this.user?.address, [
        Validators.required,
      ]),
      description: this.formBuilder.control(this.user?.description),
    });

    this.fullname = this.form.get('fullname');
    this.phoneNumber = this.form.get('phoneNumber');
    this.dateOfBirth = this.form.get('dateOfBirth');
    this.address = this.form.get('address');
    this.description = this.form.get('description');
  }

  onUpdate() {
    if (this.form.valid && this.user) {
      const user: User = {
        ...this.form.value,
      };
      this.userService
        .update(this.user?.id, user)
        .pipe(
          takeUntilDestroyed(this.destroyRef),
          catchError(() => of(null))
        )
        .subscribe((response) => {
          if (response?.success) {
            this.toastService.success('Cập nhật thông tin cá nhân thành công!');
            this.userService
              .init(true)
              .pipe(
                takeUntilDestroyed(this.destroyRef),
                catchError(() => of(null))
              )
              .subscribe();
          }
        });
    }
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

  errorForAddress(): string {
    if (this.address?.hasError('required')) {
      return 'Địa chỉ cụ thể không được để trống!';
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
}
