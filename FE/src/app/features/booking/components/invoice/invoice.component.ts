import { CommonModule } from '@angular/common';
import { Component, DestroyRef, inject, Input } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { Router } from '@angular/router';
import { PresenceService } from '@core/services/presence.service';
import { UserService } from '@core/services/user.service';
import { Booking } from '@features/booking/models/booking.model';
import { Invoice, StatusInvoice } from '@features/booking/models/invoice.model';
import { PaymentService } from '@features/booking/services/payment.service';
import { Notice } from '@features/notification/models/notification.model';
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
  user = this.userService.currentUser;
  router = inject(Router);

  constructor(
    private paymentService: PaymentService,
    private toastService: ToastService,
    private presenceService: PresenceService,
    private userService: UserService
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
    return 'Hết hạn';
  }

  onPayment(invoice: Invoice) {
    if (invoice) {
      debugger;
      const dueDate = new Date(invoice.dueDate);
      if (dueDate >= new Date()) {
        this.paymentService
          .insert(invoice.id)
          .pipe(
            takeUntilDestroyed(this.destroyRef),
            catchError(() => of(null))
          )
          .subscribe(async (response) => {
            if (response?.success) {
              await this.presenceService.createNotification({
                receiverId: this.data?.bookingDetails![0].room?.landlordId,
                title: 'Hẹn lịch xem phòng',
                content: `${this.user()?.fullName} đã ký hợp đồng thuê phòng.`,
              } as Notice);
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
      } else {
        this.toastService.warn('Đã quá hạn thanh toán!');
      }
    }
  }
}
