import { CommonModule } from '@angular/common';
import { Component, DestroyRef, inject, Input, OnInit } from '@angular/core';
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
import { Place } from '@features/post/models/place.model';
import { Room } from '@features/apartment/models/room.model';
import { FileUploader, FileUploadModule } from 'ng2-file-upload';
import { catchError, firstValueFrom, of } from 'rxjs';
import { RoomService } from '@features/apartment/services/room.service';
import { Result } from '@core/models/result.model';
import { Photo } from '@features/apartment/models/photo.model';
import { MiniLoadComponent } from '@shared/components/mini-load/mini-load.component';
import { environment } from '@environment/environment.development';
import { CookieService } from '@core/services/cookie.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-apartment-update',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    MatSelectModule,
    CommonModule,
    FileUploadModule,
    MiniLoadComponent,
  ],
  templateUrl: './apartment-update.component.html',
  styleUrl: './apartment-update.component.scss',
})
export class ApartmentUpdateComponent implements OnInit {
  @Input() data!: Room;

  destroyRef = inject(DestroyRef);
  activeModal = inject(NgbActiveModal);

  private baseUrl = environment.apiRoot;
  uploader!: FileUploader;
  hasBaseDropzoneOver = false;
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
  condition?: AbstractControl | null;
  files: File[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private roomService: RoomService,
    private cookie: CookieService
  ) {}

  ngOnInit() {
    this.initializeUploader();
    this.form = this.formBuilder.group({
      name: this.formBuilder.control(this.data.name, [Validators.required]),
      category: this.formBuilder.control(this.data.category),
      condition: this.formBuilder.control(this.data.condition),
      address: this.formBuilder.control(this.data.address, [
        Validators.required,
      ]),
      province: this.formBuilder.control('', [Validators.required]),
      district: this.formBuilder.control(''),
      ward: this.formBuilder.control(''),
      bedroom: this.formBuilder.control(this.data.bedroom, [
        Validators.required,
      ]),
      toilet: this.formBuilder.control(this.data.toilet, [Validators.required]),
      interior: this.formBuilder.control(this.data.interior),
      bathroom: this.formBuilder.control(this.data.bathroom, [
        Validators.required,
      ]),
      price: this.formBuilder.control(this.data.price, [
        Validators.required,
        Validators.min(1),
      ]),
      area: this.formBuilder.control(this.data.area, [
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
  }

  fileOverBase(e: any) {
    this.hasBaseDropzoneOver = e;
  }

  accept() {
    this.onUpdate();
  }

  decline() {
    this.activeModal.dismiss(false);
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      url: `${this.baseUrl}/api/v1/rooms/insert-photo/${this.data.id}`,
      authToken: `Bearer ${this.cookie.get('token')}`,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024,
    });

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    };

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const res: Result<Photo> = JSON.parse(response);
        this.data.photos?.push(res.data);
      }
    };
  }

  onUpdate() {
    if (this.form.valid) {
      const room: Room = {
        ...this.form.value,
        province: this.province?.value.name,
        district: this.district?.value.name,
        ward: this.ward?.value?.name,
      };
      this.roomService
        .update(this.data.id, room)
        .pipe(
          takeUntilDestroyed(this.destroyRef),
          catchError(() => of(null))
        )
        .subscribe((response) => {
          if (response?.success) {
            this.activeModal.close(true);
          }
        });
    }
  }

  onDeletePhoto(photoId: string) {
    this.roomService.deletePhoto(this.data.id, photoId).subscribe(() => {
      this.data.photos = this.data.photos?.filter((x) => x.id !== photoId);
    });
  }

  errorForName(): string {
    if (this.name?.hasError('required')) {
      return 'Tên phòng trọ không được để trống!';
    }
    return '';
  }

  errorForProvince(): string {
    if (this.category?.hasError('required')) {
      return 'Vui lòng chọn Tỉnh/Thành!';
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
