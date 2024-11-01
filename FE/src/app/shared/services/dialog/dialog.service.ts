import { inject, Injectable } from '@angular/core';
import { Room } from '@features/apartment/models/room.model';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmModalComponent } from '@shared/components/modal/confirm-modal/confirm-modal.component';
import { Confirm } from '@shared/models/modal.model';

@Injectable({
  providedIn: 'root',
})
export class DialogService {
  modalService = inject(NgbModal);

  confirm(modal: Confirm): Promise<boolean> {
    const modalRef = this.modalService.open(ConfirmModalComponent);
    modalRef.componentInstance.modal = modal;
    return modalRef.result;
  }

  view(modal: any, data: any, size: string = 'lg') {
    const modalRef = this.modalService.open(modal, { size });
    if (data) {
      modalRef.componentInstance.data = data;
    }
  }

  form(modal: any, data: any, size: string = 'xl'): Promise<boolean> {
    const modalRef = this.modalService.open(modal, { size });
    if (data) {
      modalRef.componentInstance.data = data;
    }
    return modalRef.result;
  }
}
