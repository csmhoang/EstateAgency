import { Component, inject, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { Booking, StatusBooking } from '@features/booking/models/booking.model';

@Component({
  selector: 'app-booking-view',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './booking-view.component.html',
  styleUrl: './booking-view.component.scss'
})
export class BookingViewComponent {
  @Input() data!: Booking;
  activeModal = inject(NgbActiveModal);
  StatusBookingFilter = StatusBooking;
  router = inject(Router);

  decline() {
    this.activeModal.dismiss(false);
  }

  onMove(postId?: string) {
    this.router.navigate(['/apartment/detail', postId]);
  }
}
