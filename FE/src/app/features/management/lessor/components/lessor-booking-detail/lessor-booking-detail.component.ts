import { CommonModule } from '@angular/common';
import { Component, inject, Input } from '@angular/core';
import { Booking, StatusBooking } from '@features/booking/models/booking.model';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-lessor-booking-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './lessor-booking-detail.component.html',
  styleUrl: './lessor-booking-detail.component.scss',
})
export class LessorBookingDetailComponent {
  @Input() data!: Booking;
  activeModal = inject(NgbActiveModal);
  StatusBookingFilter = StatusBooking;

  decline() {
    this.activeModal.dismiss(false);
  }
}
