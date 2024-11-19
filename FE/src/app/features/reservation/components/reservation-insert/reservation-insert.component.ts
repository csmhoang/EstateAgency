import { CommonModule } from '@angular/common';
import { Component, DestroyRef, inject, Input, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import {
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
import { Post } from '@features/post/models/post.model';
import { Reservation } from '@features/reservation/models/reservation.model';
import { ReservationService } from '@features/reservation/services/reservation.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
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
    MatSelectModule,
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

  constructor(
    private formBuilder: FormBuilder,
    private reservationService: ReservationService,
    private userService: UserService
  ) {}

  ngOnInit() {
    this.form = this.formBuilder.group({
      reservationDate: this.formBuilder.control('', [Validators.required]),
      note: this.formBuilder.control(''),
    });
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
}
