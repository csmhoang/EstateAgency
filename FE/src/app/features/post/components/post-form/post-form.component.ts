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
import { MatSelectModule } from '@angular/material/select';
import { User } from '@core/models/user.model';
import { UserService } from '@core/services/user.service';
import { Place } from '@features/post/models/place.model';
import { Room } from '@features/post/models/room.model';
import { PostService } from '@features/post/services/post.service';
import { ToastService } from '@shared/services/toast/toast.service';
import { FileUploader, FileUploadModule } from 'ng2-file-upload';
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
    FileUploadModule,
  ],
  templateUrl: './post-form.component.html',
  styleUrl: './post-form.component.scss',
})
export class PostFormComponent implements OnInit {
  uploader!: FileUploader;
  hasBaseDropzoneOver = false;

  destroyRef = inject(DestroyRef);
  minDate = new Date();
  user?: User | null;
  provices$ = firstValueFrom(this.postService.getProvince());
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
  files: File[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private toastService: ToastService,
    private userService: UserService,
    public postService: PostService
  ) {}

  ngOnInit() {
    this.initializeUploader();
    this.form = this.formBuilder.group({
      name: this.formBuilder.control('', [Validators.required]),
      category: this.formBuilder.control('', [Validators.required]),
      address: this.formBuilder.control('', [Validators.required]),
      province: this.formBuilder.control(''),
      district: this.formBuilder.control(''),
      ward: this.formBuilder.control(''),
      price: this.formBuilder.control(0, [
        Validators.required,
        Validators.min(1),
      ]),
      bedroom: this.formBuilder.control(0, [Validators.required]),
      bathroom: this.formBuilder.control(0, [Validators.required]),
      area: this.formBuilder.control(0, [
        Validators.required,
        Validators.min(1),
      ]),
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
          this.postService.getDistrict(value.id)
        );
        this.wards$ = undefined;
      }
    });

    this.district?.valueChanges.subscribe((value) => {
      if (value) {
        this.wards$ = firstValueFrom(this.postService.getWard(value.id));
      }
    });

    this.userService.currentUser
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((user) => this.user = user);
  }

  fileOverBase(e: any) {
    this.hasBaseDropzoneOver = e;
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      url: '',
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024,
    });

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
      this.files.push(file._file);
    };
  }

  onPost() {
    if (this.form.valid) {
      const room: Room = {
        ...this.form.value,
        province: this.province?.value.name,
        district: this.district?.value.name,
        ward: this.ward?.value.name,
        landlordId: this.user?.id,
      };

      this.postService
        .insert(room, this.files)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe({
          next: (response) => {
            if (response.success) {
              this.toastService.success(response.messages);
            }
          },
        });
    }
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
