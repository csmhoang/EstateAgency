import { CommonModule } from '@angular/common';
import { Component, inject, Input } from '@angular/core';
import { StatusBookingDetail } from '@features/booking/models/booking-detail.model';
import { Booking, StatusBooking } from '@features/booking/models/booking.model';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-booking-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './booking-detail.component.html',
  styleUrl: './booking-detail.component.scss',
})
export class BookingDetailComponent {
  @Input() data!: Booking;
  activeModal = inject(NgbActiveModal);
  StatusBookingFilter = StatusBooking;

  statusBookingDetailFilter = StatusBookingDetail;

  decline() {
    this.activeModal.dismiss(false);
  }
}
