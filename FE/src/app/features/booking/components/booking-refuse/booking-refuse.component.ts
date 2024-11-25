import { Component, DestroyRef, inject, Input } from '@angular/core';
import { FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { catchError, of } from 'rxjs';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { BookingService } from '@features/booking/services/booking.service';

@Component({
  selector: 'app-booking-refuse',
  standalone: true,
  imports: [MatFormFieldModule, MatInputModule, ReactiveFormsModule],
  templateUrl: './booking-refuse.component.html',
  styleUrl: './booking-refuse.component.scss',
})
export class BookingRefuseComponent {
  @Input() data!: string;
  destroyRef = inject(DestroyRef);
  activeModal = inject(NgbActiveModal);

  rejectionReason = new FormControl('', [Validators.required]);

  constructor(private bookingService: BookingService) {}

  onRefuse() {
    if (this.rejectionReason.valid) {
      this.bookingService
        .refuse(this.data, this.rejectionReason.value!)
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
    this.onRefuse();
  }

  decline() {
    this.activeModal.dismiss(false);
  }
}
