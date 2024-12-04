import { CommonModule } from '@angular/common';
import { Component, DestroyRef, inject, Input } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { Router } from '@angular/router';
import { ConditionRoom } from '@features/apartment/models/room.model';
import { BookingRefuseComponent } from '@features/booking/components/booking-refuse/booking-refuse.component';
import { StatusBookingDetail } from '@features/booking/models/booking-detail.model';
import { Booking } from '@features/booking/models/booking.model';
import { BookingService } from '@features/booking/services/booking.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { DialogService } from '@shared/services/dialog/dialog.service';
import { ToastService } from '@shared/services/toast/toast.service';
import { catchError, of } from 'rxjs';

@Component({
  selector: 'app-lessor-booking-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './lessor-booking-detail.component.html',
  styleUrl: './lessor-booking-detail.component.scss',
})
export class LessorBookingDetailComponent {
  @Input() data!: Booking;
  destroyRef = inject(DestroyRef);
  activeModal = inject(NgbActiveModal);
  router = inject(Router);
  statusBookingDetailFilter = StatusBookingDetail;
  conditionRoomFilter = ConditionRoom;

  constructor(
    private dialogService: DialogService,
    private bookingService: BookingService,
    private toastService: ToastService
  ) {}

  decline() {
    this.activeModal.dismiss(false);
  }

  async onAccept(id: string, status: string, condition: string) {
    if (status === 'Pending') {
      if (condition !== 'Occupied') {
        this.bookingService
          .responseDetail(id, 'Accepted')
          .pipe(
            takeUntilDestroyed(this.destroyRef),
            catchError(() => of(null))
          )
          .subscribe((response) => {
            if (response?.success && this.data.bookingDetails) {
              this.toastService.success('Đã chấp nhận đặt phòng');
              const len = this.data.bookingDetails.length;
              for (let i = 0; i < len; i++) {
                if (this.data.bookingDetails[i].id === id) {
                  this.data.bookingDetails[i].status = 'Accepted';
                  break;
                }
              }
              let isConfirm = false;
              for (let i = 0; i < len; i++) {
                let status = this.data.bookingDetails[i].status;
                if (status === 'Pending') break;
                if (status === 'Accepted') isConfirm = true;
                if (len === i + 1) {
                  this.bookingService
                    .response(
                      this.data.id!,
                      isConfirm ? 'Confirmed' : 'Rejected'
                    )
                    .pipe(
                      takeUntilDestroyed(this.destroyRef),
                      catchError(() => of(null))
                    )
                    .subscribe((response) => {
                      if (response?.success) {
                        this.router
                          .navigateByUrl('/dummy', { skipLocationChange: true })
                          .then(() => {
                            this.router.navigateByUrl('/lessor/booking');
                          });
                      }
                    });
                }
              }
            }
          });
      } else {
        this.toastService.warn('Phòng không có sẵn.');
      }
    } else {
      this.toastService.warn('Bạn không thể chấp nhận phòng lúc này!');
    }
  }

  onRefuse(id: string, status: string) {
    if (status === 'Pending') {
      this.dialogService
        .form(BookingRefuseComponent, id, 'md')
        .then(async () => {
          if (this.data.bookingDetails) {
            this.toastService.success('Đã từ chối đặt phòng');
            for (let i = 0; i < this.data.bookingDetails.length; i++) {
              if (this.data.bookingDetails[i].id === id) {
                this.data.bookingDetails[i].status = 'Rejected';
                break;
              }
            }
          }
        });
    } else {
      this.toastService.warn('Bạn không thể từ chối đặt phòng lúc này!');
    }
  }
}
