import { CommonModule } from '@angular/common';
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
import { UserService } from '@core/services/user.service';
import { Post } from '@features/post/models/post.model';
import { Reservation } from '@features/reservation/models/reservation.model';
import { ReservationService } from '@features/reservation/services/reservation.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastService } from '@shared/services/toast/toast.service';
import { catchError, of } from 'rxjs';

@Component({
  selector: 'app-reservation-insert',
  standalone: true,
  providers: [provideNativeDateAdapter()],
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    ReactiveFormsModule,
    CommonModule,
  ],
  templateUrl: './reservation-insert.component.html',
  styleUrl: './reservation-insert.component.scss',
})
export class ReservationInsertComponent implements OnInit {
  @Input() data!: Post;
  user = this.userService.currentUser();
  minDate = new Date();

  destroyRef = inject(DestroyRef);
  activeModal = inject(NgbActiveModal);

  form: FormGroup = new FormGroup({});

  reservationHour?: AbstractControl | null;
  reservationMinute?: AbstractControl | null;

  constructor(
    private formBuilder: FormBuilder,
    private reservationService: ReservationService,
    private toastService: ToastService,
    private userService: UserService
  ) {}

  ngOnInit() {
    this.form = this.formBuilder.group({
      reservationDate: this.formBuilder.control('', [Validators.required]),
      reservationHour: this.formBuilder.control('', [Validators.required]),
      reservationMinute: this.formBuilder.control('', [Validators.required]),
      note: this.formBuilder.control(''),
    });

    this.reservationHour = this.form.get('reservationHour');
    this.reservationMinute = this.form.get('reservationMinute');
  }

  onReservation() {
    if (this.form.valid && this.user) {
      const reservation: Reservation = {
        ...this.form.value,
        postId: this.data.id,
        tenantId: this.user.id,
      };
      this.reservationService
        .insert(reservation)
        .pipe(
          takeUntilDestroyed(this.destroyRef),
          catchError(() => of(null))
        )
        .subscribe((response) => {
          if (response?.success) {
            if (response?.statusCode === 205) {
              this.toastService.warn(
                'Đã có người đặt lịch vào thời gian này, vui lòng chọn lại!'
              );
              return;
            }
            this.activeModal.close(true);
          }
        });
    }
  }

  accept() {
    this.onReservation();
  }

  decline() {
    this.activeModal.dismiss(false);
  }

  errorForHour(): string {
    if (this.reservationHour?.hasError('required')) {
      return 'Giờ không được để trống!';
    }
    return '';
  }

  errorForMinute(): string {
    if (this.reservationMinute?.hasError('required')) {
      return 'Phút không được để trống!';
    }
    return '';
  }
}
