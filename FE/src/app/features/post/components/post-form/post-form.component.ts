import { CommonModule } from '@angular/common';
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
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectChange, MatSelectModule } from '@angular/material/select';
import { Place } from '@features/post/models/place.model';
import { PostFormService } from '@features/post/services/post-form.service';
import { ToastService } from '@shared/services/toast/toast.service';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-post-form',
  standalone: true,
  providers: [provideNativeDateAdapter()],
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    ReactiveFormsModule,
    MatRadioModule,
    MatSelectModule,
    CommonModule,
  ],
  templateUrl: './post-form.component.html',
  styleUrl: './post-form.component.scss',
})
export class PostFormComponent implements OnInit {
  destroyRef = inject(DestroyRef);
  minDate = new Date();
  provices$ = firstValueFrom(this.postFormService.getProvince());
  districts$?: Promise<Place[]>;
  wards$?: Promise<Place[]>;

  isDisable = true;

  form: FormGroup = new FormGroup({});

  name?: AbstractControl | null;
  category?: AbstractControl | null;
  address?: AbstractControl | null;
  province?: AbstractControl | null;
  district?: AbstractControl | null;
  ward?: AbstractControl | null;
  price?: AbstractControl | null;
  bedroom?: AbstractControl | null;
  bathroom?: AbstractControl | null;
  area?: AbstractControl | null;
  description?: AbstractControl | null;
  availableFrom?: AbstractControl | null;

  constructor(
    private formBuilder: FormBuilder,
    private toastService: ToastService,
    public postFormService: PostFormService
  ) {}

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      name: this.formBuilder.control('', [Validators.required]),
      category: this.formBuilder.control('', [Validators.required]),
      address: this.formBuilder.control('', [Validators.required]),
      province: this.formBuilder.control(''),
      district: this.formBuilder.control(''),
      ward: this.formBuilder.control(''),
      price: this.formBuilder.control(0, [Validators.required, Validators.min(1)]),
      bedroom: this.formBuilder.control(0, [Validators.required]),
      bathroom: this.formBuilder.control(0, [Validators.required]),
      area: this.formBuilder.control(0, [Validators.required, Validators.min(1)]),
      description: this.formBuilder.control(''),
      availableFrom: this.formBuilder.control(new Date()),
    });

    this.name = this.form.get('name');
    this.category = this.form.get('category');
    this.address = this.form.get('address');
    this.province = this.form.get('province');
    this.district = this.form.get('district');
    this.ward = this.form.get('ward');
    this.price = this.form.get('price');
    this.bedroom = this.form.get('bedroom');
    this.bathroom = this.form.get('bathroom');
    this.area = this.form.get('area');
    this.description = this.form.get('description');
    this.availableFrom = this.form.get('availableFrom');

    this.province?.valueChanges.subscribe((value) => {
      if (value) {
        this.districts$ = firstValueFrom(
          this.postFormService.getDistrict(value.id)
        );
        this.wards$ = undefined;
        console.log(value)
      }
    });

    this.district?.valueChanges.subscribe((value) => {
      if (value) {
        this.wards$ = firstValueFrom(this.postFormService.getWard(value.id));
      }
    });
  }

  onPost() {
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

  errorForName(): string {
    if (this.name?.hasError('required')) {
      return 'Tiêu đề không được để trống!';
    }
    return '';
  }

  errorForCategory(): string {
    if (this.category?.hasError('required')) {
      return 'Vui lòng chọn loại nhà đất!';
    }
    return '';
  }

  errorForAddress(): string {
    if (this.address?.hasError('required')) {
      return 'Địa chỉ cụ thể không được để trống!';
    }
    return '';
  }

  errorForBedroom(): string {
    if (this.bedroom?.hasError('required')) {
      return 'Số phòng ngủ không được để trống!';
    }
    return '';
  }

  errorForBathroom(): string {
    if (this.bathroom?.hasError('required')) {
      return 'Số phòng tắm không được để trống!';
    }
    return '';
  }

  errorForArea(): string {
    if (this.area?.hasError('required')) {
      return 'Diện tích không được để trống!';
    }
    if (this.area?.hasError('min')) {
      return 'Diện tích phải lớn hơn 0';
    }
    return '';
  }

  errorForPrice(): string {
    if (this.price?.hasError('required')) {
      return 'Giá không được để trống!';
    }
    if (this.price?.hasError('min')) {
      return 'Giá phải lớn hơn 0';
    }
    return '';
  }
}
