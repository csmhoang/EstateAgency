import { inject, Injectable } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ModalComponent } from '@shared/components/modal/modal.component';
import { Modal } from '@shared/models/modal.model';

@Injectable({
  providedIn: 'root',
})
export class DialogService {
  modalService = inject(NgbModal);

  confirm(modal: Modal): Promise<boolean> {
    const modalRef = this.modalService.open(ModalComponent);
    modalRef.componentInstance.modal = modal;
    return modalRef.result;
  }

  form(modal: Modal): Promise<boolean> {
    const modalRef = this.modalService.open(ModalComponent, { size: 'xl' });
    modalRef.componentInstance.modal = modal;
    return modalRef.result;
  }
}
