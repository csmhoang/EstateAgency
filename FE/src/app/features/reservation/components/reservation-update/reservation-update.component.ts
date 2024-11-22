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
import { catchError, of } from 'rxjs';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ReservationService } from '@features/reservation/services/reservation.service';
import { Reservation } from '@features/reservation/models/reservation.model';
import { provideNativeDateAdapter } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';

@Component({
  selector: 'app-reservation-update',
  standalone: true,
  providers: [provideNativeDateAdapter()],
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    ReactiveFormsModule,
  ],
  templateUrl: './reservation-update.component.html',
  styleUrl: './reservation-update.component.scss',
})
export class ReservationUpdateComponent implements OnInit {
  @Input() data!: Reservation;

  minDate = new Date();
  destroyRef = inject(DestroyRef);
  activeModal = inject(NgbActiveModal);

  form: FormGroup = new FormGroup({});
  reservationHour?: AbstractControl | null;
  reservationMinute?: AbstractControl | null;

  constructor(
    private formBuilder: FormBuilder,
    private reservationService: ReservationService
  ) {}

  ngOnInit() {
    this.form = this.formBuilder.group({
      reservationDate: this.formBuilder.control(this.data.reservationDate, [
        Validators.required,
      ]),
      reservationHour: this.formBuilder.control(this.data.reservationHour, [
        Validators.required,
      ]),
      reservationMinute: this.formBuilder.control(this.data.reservationMinute, [
        Validators.required,
      ]),
      note: this.formBuilder.control(this.data?.note),
    });

    this.reservationHour = this.form.get('reservationHour');
    this.reservationMinute = this.form.get('reservationMinute');
  }

  onUpdate() {
    if (this.form.valid) {
      const reservation: Reservation = {
        ...this.form.value,
      };
      this.reservationService
        .update(this.data.id, reservation)
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

  accept() {
    this.onUpdate();
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
