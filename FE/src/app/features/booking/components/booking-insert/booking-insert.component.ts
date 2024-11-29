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
import { MatSelectModule } from '@angular/material/select';
import { UserService } from '@core/services/user.service';
import { Booking } from '@features/booking/models/booking.model';
import { BookingService } from '@features/booking/services/booking.service';
import { Post } from '@features/post/models/post.model';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { catchError, of } from 'rxjs';

@Component({
  selector: 'app-booking-insert',
  standalone: true,
  providers: [provideNativeDateAdapter()],
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    ReactiveFormsModule,
    MatSelectModule,
    CommonModule,
  ],
  templateUrl: './booking-insert.component.html',
  styleUrl: './booking-insert.component.scss',
})
export class BookingInsertComponent implements OnInit {
  @Input() data!: Post;
  user = this.userService.currentUser();
  minIntendedIntoDate = this.data.availableFrom;
  minEndDate = new Date(
    new Date(this.data.availableFrom).setMonth(new Date().getMonth() + 1)
  );

  destroyRef = inject(DestroyRef);
  activeModal = inject(NgbActiveModal);

  form: FormGroup = new FormGroup({});
  numberOfTenant?: AbstractControl | null;

  constructor(
    private formBuilder: FormBuilder,
    private bookingService: BookingService,
    private userService: UserService
  ) {}

  ngOnInit() {
    this.form = this.formBuilder.group({
      intendedIntoDate: this.formBuilder.control('', [Validators.required]),
      endDate: this.formBuilder.control('', [Validators.required]),
      numberOfTenant: this.formBuilder.control(0, [Validators.required]),
      note: this.formBuilder.control(''),
    });

    this.numberOfTenant = this.form.get('numberOfTenant');
  }

  onBooking() {
    if (this.form.valid && this.user) {
      const booking: Booking = {
        ...this.form.value,
        roomId: this.data.room?.id,
        tenantId: this.user.id,
      };
      this.bookingService
        .insert(booking)
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
    this.onBooking();
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
