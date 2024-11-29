import { Component, DestroyRef, inject, Input } from '@angular/core';
import { FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { catchError, of } from 'rxjs';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ReservationService } from '@features/reservation/services/reservation.service';

@Component({
  selector: 'app-reservation-refuse',
  standalone: true,
  imports: [MatFormFieldModule, MatInputModule, ReactiveFormsModule],
  templateUrl: './reservation-refuse.component.html',
  styleUrl: './reservation-refuse.component.scss',
})
export class ReservationRefuseComponent {
  @Input() data!: string;
  destroyRef = inject(DestroyRef);
  activeModal = inject(NgbActiveModal);

  rejectionReason = new FormControl('', [Validators.required]);

  constructor(private reservationService: ReservationService) {}

  onRefuse() {
    if (this.rejectionReason.valid) {
      this.reservationService
        .response(this.data, 'Rejected', this.rejectionReason.value!)
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
