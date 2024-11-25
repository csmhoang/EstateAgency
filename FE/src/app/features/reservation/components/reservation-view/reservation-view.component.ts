import { Component, inject, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import {
  Reservation,
  StatusReservation,
} from '@features/reservation/models/reservation.model';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-reservation-view',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './reservation-view.component.html',
  styleUrl: './reservation-view.component.scss',
})
export class ReservationViewComponent {
  @Input() data!: Reservation;
  activeModal = inject(NgbActiveModal);
  statusReservationFilter = StatusReservation;
  router = inject(Router);

  decline() {
    this.activeModal.dismiss(false);
  }
}
