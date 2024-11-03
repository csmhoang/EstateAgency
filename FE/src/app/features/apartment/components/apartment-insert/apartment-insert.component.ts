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
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { User } from '@core/models/user.model';
import { UserService } from '@core/services/user.service';
import { Place } from '@features/post/models/place.model';
import { Room } from '@features/apartment/models/room.model';
import { ToastService } from '@shared/services/toast/toast.service';
import { FileUploader, FileUploadModule } from 'ng2-file-upload';
import { catchError, firstValueFrom, of } from 'rxjs';
import { RoomService } from '@features/apartment/services/room.service';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-apartment-insert',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    MatSelectModule,
    CommonModule,
    FileUploadModule,
    RouterLink,
  ],
  templateUrl: './apartment-insert.component.html',
  styleUrl: './apartment-insert.component.scss',
})
export class ApartmentInsertComponent implements OnInit {
  uploader!: FileUploader;
  hasBaseDropzoneOver = false;

  destroyRef = inject(DestroyRef);
  user?: User | null;
  provices$ = firstValueFrom(
    this.roomService.getProvince().pipe(
      takeUntilDestroyed(this.destroyRef),
      catchError(() => of(null))
    )
  );
  districts$?: Promise<Place[] | null>;
  wards$?: Promise<Place[] | null>;

  form: FormGroup = new FormGroup({});

  name?: AbstractControl | null;
  category?: AbstractControl | null;
  address?: AbstractControl | null;
  province?: AbstractControl | null;
  district?: AbstractControl | null;
  ward?: AbstractControl | null;
  bedroom?: AbstractControl | null;
  bathroom?: AbstractControl | null;
  toilet?: AbstractControl | null;
  interior?: AbstractControl | null;
  area?: AbstractControl | null;
  price?: AbstractControl | null;
  files: File[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private toastService: ToastService,
    private userService: UserService,
    private router: Router,
    private roomService: RoomService
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
      bedroom: this.formBuilder.control(0, [Validators.required]),
      toilet: this.formBuilder.control(0, [Validators.required]),
      interior: this.formBuilder.control('', [Validators.required]),
      bathroom: this.formBuilder.control(0, [Validators.required]),
      price: this.formBuilder.control(0, [
        Validators.required,
        Validators.min(1),
      ]),
      area: this.formBuilder.control(0, [
        Validators.required,
        Validators.min(1),
      ]),
    });

    this.name = this.form.get('name');
    this.category = this.form.get('category');
    this.address = this.form.get('address');
    this.province = this.form.get('province');
    this.district = this.form.get('district');
    this.ward = this.form.get('ward');
    this.bedroom = this.form.get('bedroom');
    this.bathroom = this.form.get('bathroom');
    this.price = this.form.get('price');
    this.area = this.form.get('area');

    this.province?.valueChanges.subscribe((value) => {
      if (value) {
        this.districts$ = firstValueFrom(
          this.roomService.getDistrict(value.id).pipe(
            takeUntilDestroyed(this.destroyRef),
            catchError(() => of(null))
          )
        );
        this.wards$ = undefined;
      }
    });

    this.district?.valueChanges.subscribe((value) => {
      if (value) {
        this.wards$ = firstValueFrom(
          this.roomService.getWard(value.id).pipe(
            takeUntilDestroyed(this.destroyRef),
            catchError(() => of(null))
          )
        );
      }
    });

    this.userService.currentUser
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((user) => (this.user = user));
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

  onInsert() {
    if (this.form.valid && this.user) {
      const room: Room = {
        ...this.form.value,
        province: this.province?.value.name,
        district: this.district?.value.name,
        ward: this.ward?.value.name,
        landlordId: this.user?.id,
      };

      this.roomService
        .insert(room, this.files)
        .pipe(
          takeUntilDestroyed(this.destroyRef),
          catchError(() => of(null))
        )
        .subscribe({
          next: (response) => {
            if (response?.success) {
              this.toastService.success('Thêm phòng thành công');
              void this.router.navigate(['/lessor/apartment']);
            }
          },
        });
    }
  }

  errorForName(): string {
    if (this.name?.hasError('required')) {
      return 'Tên phòng trọ không được để trống!';
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
  errorForToilet(): string {
    if (this.toilet?.hasError('required')) {
      return 'Số nhà vệ sinh không được để trống!';
    }
    return '';
  }

  errorForBathroom(): string {
    if (this.bathroom?.hasError('required')) {
      return 'Số phòng tắm không được để trống!';
    }
    return '';
  }

  errorForInterior(): string {
    if (this.interior?.hasError('required')) {
      return 'Vui lòng chọn nội thất!';
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
