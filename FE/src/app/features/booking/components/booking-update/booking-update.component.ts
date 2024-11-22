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
import { Booking } from '@features/booking/models/booking.model';
import { BookingService } from '@features/booking/services/booking.service';

@Component({
  selector: 'app-booking-update',
  standalone: true,
  providers: [provideNativeDateAdapter()],
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    ReactiveFormsModule,
  ],
  templateUrl: './booking-update.component.html',
  styleUrl: './booking-update.component.scss',
})
export class BookingUpdateComponent implements OnInit {
  @Input() data!: Booking;

  minIntendedIntoDate = new Date();
  minEndDate = new Date().setMonth(new Date().getMonth() + 1);
  destroyRef = inject(DestroyRef);
  activeModal = inject(NgbActiveModal);

  form: FormGroup = new FormGroup({});
  numberOfTenant?: AbstractControl | null;

  constructor(
    private formBuilder: FormBuilder,
    private bookingService: BookingService
  ) {}

  ngOnInit() {
    this.form = this.formBuilder.group({
      intendedIntoDate: this.formBuilder.control(this.data.intendedIntoDate, [
        Validators.required,
      ]),
      endDate: this.formBuilder.control(this.data.endDate, [
        Validators.required,
      ]),
      numberOfTenant: this.formBuilder.control(this.data.numberOfTenant, [
        Validators.required,
      ]),
      note: this.formBuilder.control(this.data.note),
    });

    this.numberOfTenant = this.form.get('numberOfTenant');
  }

  onUpdate() {
    if (this.form.valid) {
      const booking: Booking = {
        ...this.form.value,
      };
      this.bookingService
        .update(this.data.id!, booking)
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

  errorForNumberOfTenant(): string {
    if (this.numberOfTenant?.hasError('required')) {
      return 'Số người ở không được để trống!';
    }
    return '';
  }
}
