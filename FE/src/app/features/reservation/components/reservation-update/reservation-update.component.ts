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
    CommonModule,
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
  note?: AbstractControl | null;
  date?: AbstractControl | null;
  hour?: AbstractControl | null;
  minute?: AbstractControl | null;

  constructor(
    private formBuilder: FormBuilder,
    private reservationService: ReservationService
  ) {}

  ngOnInit() {
    this.form = this.formBuilder.group({
      date: this.formBuilder.control(this.data.reservationDate.getDate(), [
        Validators.required,
      ]),
      hour: this.formBuilder.control(this.data.reservationDate.getHours(), [
        Validators.required,
      ]),
      minute: this.formBuilder.control(this.data.reservationDate.getMinutes(), [
        Validators.required,
      ]),
      note: this.formBuilder.control(this.data?.note),
    });

    this.note = this.form.get('note');
    this.date = this.form.get('date');
    this.hour = this.form.get('hour');
    this.minute = this.form.get('minute');
  }

  onUpdate() {
    if (this.form.valid) {
      const reservationDate = new Date(this.date?.value);
      reservationDate.setHours(this.hour?.value, this.minute?.value);
      const reservation: Reservation = {
        ...this.form.value,
        reservationDate: reservationDate,
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
    if (this.hour?.hasError('required')) {
      return 'Giờ không được để trống!';
    }
    return '';
  }

  errorForMinute(): string {
    if (this.minute?.hasError('required')) {
      return 'Phút không được để trống!';
    }
    return '';
  }
}
