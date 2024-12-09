import { CommonModule } from '@angular/common';
import { Component, DestroyRef, inject, Input } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { Router } from '@angular/router';
import { StatusBookingDetail } from '@features/booking/models/booking-detail.model';
import { Booking, StatusBooking } from '@features/booking/models/booking.model';
import { BookingDetailService } from '@features/booking/services/booking-detail.service';
import { BookingService } from '@features/booking/services/booking.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastService } from '@shared/services/toast/toast.service';
import { catchError, debounceTime, of } from 'rxjs';

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
  router = inject(Router);
  StatusBookingFilter = StatusBooking;

  statusBookingDetailFilter = StatusBookingDetail;

  constructor(
    private bookingService: BookingService,
    private toastService: ToastService,
    private bookingDetailService:BookingDetailService
  ) {}

  decline() {
    this.activeModal.dismiss(false);
  }

  async onCancel(id: string, status: string) {
    if (status === 'Pending') {
      this.bookingDetailService
        .responseDetail(id, 'Canceled')
        .pipe(
          takeUntilDestroyed(this.destroyRef),
          catchError(() => of(null))
        )
        .subscribe((response) => {
          if (response?.success && this.data.bookingDetails) {
            const len = this.data.bookingDetails.length;

            for (let i = 0; i < len; i++) {
              if (this.data.bookingDetails[i].id === id) {
                this.data.bookingDetails[i].status = 'Canceled';
                break;
              }
            }
            for (let i = 0; i < len; i++) {
              let status = this.data.bookingDetails[i].status;
              if (status !== 'Canceled') break;
              if (len === i + 1) {
                this.bookingService
                  .response(this.data.id!, 'Canceled')
                  .pipe(
                    debounceTime(1000),
                    takeUntilDestroyed(this.destroyRef),
                    catchError(() => of(null))
                  )
                  .subscribe((response) => {
                    if (response?.success) {
                      this.activeModal.dismiss(false);
                      this.router
                        .navigateByUrl('/dummy', { skipLocationChange: true })
                        .then(() => {
                          this.router.navigateByUrl('/profile/booking');
                        });
                    }
                  });
              }
            }
          }
        });
    } else {
      this.toastService.warn('Bạn không thể hủy đặt phòng lúc này!');
    }
  }
}
