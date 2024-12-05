import { CommonModule } from '@angular/common';
import { Component, DestroyRef, inject, Input } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { Router } from '@angular/router';
import { Booking } from '@features/booking/models/booking.model';
import { StatusInvoice } from '@features/booking/models/invoice.model';
import { InvoiceService } from '@features/booking/services/invoice.service';
import { PaymentService } from '@features/booking/services/payment.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastService } from '@shared/services/toast/toast.service';
import { catchError, of } from 'rxjs';

@Component({
  selector: 'app-invoice',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './invoice.component.html',
  styleUrl: './invoice.component.scss',
})
export class InvoiceComponent {
  @Input() data!: Booking;
  statusFilter = StatusInvoice;
  destroyRef = inject(DestroyRef);
  activeModal = inject(NgbActiveModal);
  router = inject(Router);

  constructor(
    private paymentService: PaymentService,
    private toastService: ToastService,
    private invoiceService: InvoiceService
  ) {}

  countDownFilter(time: Date) {
    const now = new Date();
    const seconds = Math.floor(
      (new Date(time).getTime() - now.getTime()) / 1000
    );

    const intervals: { [key: string]: number } = {
      ngày: 86400,
      giờ: 3600,
      phút: 60,
      giây: 1,
    };

    for (const [unit, value] of Object.entries(intervals)) {
      const interval = Math.floor(seconds / value);
      if (interval >= 1) {
        return `${interval} ${unit}`;
      }
    }
    if (this.data.invoice?.status !== 'Overdue') {
      this.invoiceService
        .response(this.data.invoiceId, 'Overdue')
        .pipe(
          takeUntilDestroyed(this.destroyRef),
          catchError(() => of(null))
        )
        .subscribe((response) => {
          if (response?.success) {
            this.router
              .navigateByUrl('/dummy', { skipLocationChange: true })
              .then(() => {
                this.router.navigateByUrl('/profile/booking');
                this.activeModal.dismiss(false);
              });
          }
        });
    }
    return 'Hết hạn';
  }

  onPayment() {
    if (this.data) {
      this.paymentService
        .insert(this.data.invoiceId)
        .pipe(
          takeUntilDestroyed(this.destroyRef),
          catchError(() => of(null))
        )
        .subscribe((response) => {
          if (response?.success) {
            this.toastService.success('Thanh toán thành công!');
            this.toastService.success('Hợp đồng của bạn đã được kích hoạt!');
            this.router
              .navigateByUrl('/dummy', { skipLocationChange: true })
              .then(() => {
                this.router.navigateByUrl('/profile/booking');
                this.activeModal.dismiss(false);
              });
          }
        });
    }
  }
}
