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
import { Router } from '@angular/router';
import { UserService } from '@core/services/user.service';
import { Booking } from '@features/booking/models/booking.model';
import { Lease } from '@features/booking/models/lease.model';
import { LeaseService } from '@features/booking/services/lease.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { catchError, of } from 'rxjs';

@Component({
  selector: 'app-lease-insert',
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
  templateUrl: './lease-insert.component.html',
  styleUrl: './lease-insert.component.scss',
})
export class LeaseInsertComponent implements OnInit {
  @Input() data?: Booking;
  destroyRef = inject(DestroyRef);
  activeModal = inject(NgbActiveModal);
  router = inject(Router);
  minDate = new Date();

  form: FormGroup = new FormGroup({});

  user = this.userSerivce.currentUser();
  lessor?: AbstractControl | null;
  lessee?: AbstractControl | null;
  terms?: AbstractControl | null;

  constructor(
    private formBuilder: FormBuilder,
    private leaseService: LeaseService,
    private userSerivce: UserService
  ) {}

  ngOnInit() {
    this.form = this.formBuilder.group({
      lessor: this.formBuilder.control(this.user?.fullName, [
        Validators.required,
      ]),
      lessee: this.formBuilder.control(this.data?.tenant?.fullName, [
        Validators.required,
      ]),
      startDate: this.formBuilder.control('', [Validators.required]),
      terms: this.formBuilder.control('', [Validators.required]),
    });

    this.lessor = this.form.get('lessor');
    this.lessee = this.form.get('lessee');
    this.terms = this.form.get('terms');
  }

  onInsert() {
    if (this.form.valid && this.data) {
      const lease: Lease = {
        ...this.form.value,
        bookingId: this.data.id,
      };
      this.leaseService
        .insert(lease)
        .pipe(
          takeUntilDestroyed(this.destroyRef),
          catchError(() => of(null))
        )
        .subscribe((response) => {
          if (response?.success) {
            this.router
              .navigateByUrl('/dummy', { skipLocationChange: true })
              .then(() => {
                this.router.navigateByUrl('/lessor/booking');
              });
            this.activeModal.close(true);
          }
        });
    }
  }

  accept() {
    this.onInsert();
  }

  decline() {
    this.activeModal.dismiss(false);
  }

  errorForLessor(): string {
    if (this.lessor?.hasError('required')) {
      return 'Tên người cho thuê không được để trống!';
    }
    return '';
  }

  errorForLessee(): string {
    if (this.lessee?.hasError('required')) {
      return 'Tên người thuê không được để trống!';
    }
    return '';
  }

  errorForTerms(): string {
    if (this.terms?.hasError('required')) {
      return 'Các điều khoản không được để trống!';
    }
    return '';
  }
}
