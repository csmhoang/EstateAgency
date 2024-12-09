import { CommonModule } from '@angular/common';
import { Component, DestroyRef, inject, Input, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { Booking } from '@features/booking/models/booking.model';
import { StatusLease } from '@features/booking/models/lease.model';
import { LeaseService } from '@features/booking/services/lease.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { DialogService } from '@shared/services/dialog/dialog.service';
import { ToastService } from '@shared/services/toast/toast.service';
import { catchError, of } from 'rxjs';
import { InvoiceComponent } from '../invoice/invoice.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-lease-view',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './lease-view.component.html',
  styleUrl: './lease-view.component.scss',
})
export class LeaseViewComponent implements OnInit {
  @Input() data?: Booking | null;
  destroyRef = inject(DestroyRef);
  activeModal = inject(NgbActiveModal);
  router = inject(Router);
  statusLeaseFilter = StatusLease;
  sumPrice?: number;
  constructor(
    private dialogService: DialogService,
    private toastService: ToastService,
    private leaseService: LeaseService
  ) {}
  ngOnInit() {
    this.sumPrice = this.data?.lease?.leaseDetails?.reduce(
      (accumulator, leaseDetail) => accumulator + leaseDetail.price,
      0
    );
  }

  onReject() {
    if (this.data?.lease) {
      this.leaseService
        .response(this.data.lease.id, 'Canceled')
        .pipe(
          takeUntilDestroyed(this.destroyRef),
          catchError(() => of(null))
        )
        .subscribe((response) => {
          if (response?.success) {
            this.toastService.success('Đã từ chối hợp đồng!');
            this.activeModal.dismiss(false);
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

  onAccept() {
    this.dialogService.view(InvoiceComponent, this.data, 'lg');
    this.activeModal.dismiss(false);
  }
}
