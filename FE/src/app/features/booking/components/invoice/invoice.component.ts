import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { Booking } from '@features/booking/models/booking.model';
import { StatusInvoice } from '@features/booking/models/invoice.model';

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
}
