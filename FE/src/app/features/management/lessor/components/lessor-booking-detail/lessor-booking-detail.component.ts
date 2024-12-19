import { CommonModule } from '@angular/common';
import { Component, DestroyRef, inject, Input } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { Router } from '@angular/router';
import { PresenceService } from '@core/services/presence.service';
import { UserService } from '@core/services/user.service';
import { ConditionRoom } from '@features/apartment/models/room.model';
import { BookingRefuseComponent } from '@features/booking/components/booking-refuse/booking-refuse.component';
import { StatusBookingDetail } from '@features/booking/models/booking-detail.model';
import { Booking } from '@features/booking/models/booking.model';
import { BookingDetailService } from '@features/booking/services/booking-detail.service';
import { BookingService } from '@features/booking/services/booking.service';
import { Notice } from '@features/notification/models/notification.model';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { DialogService } from '@shared/services/dialog/dialog.service';
import { ToastService } from '@shared/services/toast/toast.service';
import { catchError, debounceTime, of } from 'rxjs';

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

  user = this.userService.currentUser;
  statusBookingDetailFilter = StatusBookingDetail;
  conditionRoomFilter = ConditionRoom;

  constructor(
    private dialogService: DialogService,
    private bookingService: BookingService,
    private toastService: ToastService,
    private bookingDetailService: BookingDetailService,
    private presenceService: PresenceService,
    private userService: UserService
  ) {}

  decline() {
    this.activeModal.dismiss(false);
  }

  async onAccept(
    id: string,
    status: string,
    condition: string,
    startDate: Date,
    visibility: boolean
  ) {
    if (status === 'Pending') {
      if (condition === 'Occupied') {
        return this.toastService.warn('Phòng không có sẵn.');
      }
      if (!visibility) {
        return this.toastService.warn('Phòng đã bị xóa.');
      }

      if (new Date(startDate) >= new Date()) {
        this.bookingDetailService
          .responseDetail(id, 'Accepted')
          .pipe(
            debounceTime(1000),
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
                    .subscribe(async (response) => {
                      if (response?.success) {
                        await this.presenceService.createNotification({
                          receiverId: this.data.tenantId,
                          title: 'Đặt phòng',
                          content: `${
                            this.user()?.fullName
                          } đã phản hồi đơn đặt phòng.`,
                        } as Notice);

                        this.activeModal.dismiss(false);
                        this.router
                          .navigateByUrl('/dummy', {
                            skipLocationChange: true,
                          })
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
        this.bookingDetailService
          .responseDetail(id, 'Canceled')
          .pipe(
            takeUntilDestroyed(this.destroyRef),
            catchError(() => of(null))
          )
          .subscribe((response) => {
            if (response?.success) {
              if (this.data.bookingDetails) {
                for (let i = 0; i < this.data.bookingDetails.length; i++) {
                  if (this.data.bookingDetails[i].id === id) {
                    this.data.bookingDetails[i].status = 'Canceled';
                    break;
                  }
                }
              }
              this.toastService.warn('Đặt phòng này ngày thuê đã quá hạn.');
            }
          });
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
            const len = this.data.bookingDetails.length;
            for (let i = 0; i < len; i++) {
              if (this.data.bookingDetails[i].id === id) {
                this.data.bookingDetails[i].status = 'Rejected';
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
                  .response(this.data.id!, isConfirm ? 'Confirmed' : 'Rejected')
                  .pipe(
                    takeUntilDestroyed(this.destroyRef),
                    catchError(() => of(null))
                  )
                  .subscribe(async (response) => {
                    if (response?.success) {
                      await this.presenceService.createNotification({
                        receiverId: this.data.tenantId,
                        title: 'Đặt phòng',
                        content: `${
                          this.user()?.fullName
                        } đã phản hồi đơn đặt phòng.`,
                      } as Notice);

                      this.activeModal.dismiss(false);
                      this.router
                        .navigateByUrl('/dummy', {
                          skipLocationChange: true,
                        })
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
      this.toastService.warn('Bạn không thể từ chối đặt phòng lúc này!');
    }
  }
}
