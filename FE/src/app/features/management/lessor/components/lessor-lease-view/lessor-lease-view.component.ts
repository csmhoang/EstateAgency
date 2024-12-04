import { CommonModule } from '@angular/common';
import { Component, inject, Input, OnInit } from '@angular/core';
import { LeaseInsertComponent } from '@features/booking/components/lease-insert/lease-insert.component';
import { Booking } from '@features/booking/models/booking.model';
import { StatusLease } from '@features/booking/models/lease.model';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { DialogService } from '@shared/services/dialog/dialog.service';
import { ToastService } from '@shared/services/toast/toast.service';

@Component({
  selector: 'app-lessor-lease-view',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './lessor-lease-view.component.html',
  styleUrl: './lessor-lease-view.component.scss',
})
export class LessorLeaseViewComponent implements OnInit {
  @Input() data?: Booking | null;
  activeModal = inject(NgbActiveModal);
  statusLeaseFilter = StatusLease;
  sumPrice?: number;
  constructor(
    private dialogService: DialogService,
    private toastService: ToastService
  ) {}
  ngOnInit() {
    this.sumPrice = this.data?.lease?.leaseDetails?.reduce(
      (accumulator, leaseDetail) => accumulator + leaseDetail.price,
      0
    );
  }

  onInsert() {
    this.dialogService
      .form(LeaseInsertComponent, this.data)
      .then(async () => {
        this.toastService.success('Tạo hợp đồng thành công!');
        this.activeModal.dismiss(false);
      });
  }
}
