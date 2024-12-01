import { CommonModule } from '@angular/common';
import { Component, DestroyRef, inject, Input } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { StatusBookingDetail } from '@features/booking/models/booking-detail.model';
import { Booking, StatusBooking } from '@features/booking/models/booking.model';
import { BookingService } from '@features/booking/services/booking.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastService } from '@shared/services/toast/toast.service';
import { catchError, of } from 'rxjs';

@Component({
  selector: 'app-booking-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './booking-detail.component.html',
  styleUrl: './booking-detail.component.scss',
})
export class BookingDetailComponent {
  @Input() data!: Booking;
  destroyRef = inject(DestroyRef);
  activeModal = inject(NgbActiveModal);
  StatusBookingFilter = StatusBooking;

  statusBookingDetailFilter = StatusBookingDetail;

  constructor(
    private bookingService: BookingService,
    private toastService: ToastService
  ) {}

  decline() {
    this.activeModal.dismiss(false);
  }

  async onCancel(id: string, status: string) {
    if (status === 'Pending') {
      this.bookingService
        .responseDetail(id, "Canceled")
        .pipe(
          takeUntilDestroyed(this.destroyRef),
          catchError(() => of(null))
        )
        .subscribe((response) => {
          if (response?.success && this.data.bookingDetails) {
            for (let i = 0; i < this.data.bookingDetails.length; i++) {
              if (this.data.bookingDetails[i].id === id) {
                this.data.bookingDetails[i].status = 'Canceled';
                break;
              }
            }
            for (const bookingDetail of this.data.bookingDetails) {
              if (bookingDetail.status !== 'Canceled') break;
              this.bookingService
                .response(this.data.id!, 'Canceled')
                .pipe(
                  takeUntilDestroyed(this.destroyRef),
                  catchError(() => of(null))
                )
                .subscribe();
            }
          }
        });
    } else {
      this.toastService.warn('Bạn không thể hủy đặt phòng lúc này!');
    }
  }
}
