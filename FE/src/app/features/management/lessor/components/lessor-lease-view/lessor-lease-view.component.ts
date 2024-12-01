import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { Lease, StatusLease } from '@features/booking/models/lease.model';

@Component({
  selector: 'app-lessor-lease-view',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './lessor-lease-view.component.html',
  styleUrl: './lessor-lease-view.component.scss',
})
export class LessorLeaseViewComponent implements OnInit {
  @Input() data?: Lease | null;

  statusLeaseFilter = StatusLease;
  sumPrice?: number;
  
  ngOnInit() {
    this.sumPrice = this.data?.leaseDetails?.reduce(
      (accumulator, leaseDetail) => accumulator + leaseDetail.price,
      0
    );
  }
}
