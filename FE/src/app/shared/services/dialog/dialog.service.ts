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

  view(modal: Modal, size: string = 'lg') {
    const modalRef = this.modalService.open(ModalComponent, { size });
    modalRef.componentInstance.modal = modal;
  }

  form(modal: Modal, size: string = 'xl'): Promise<boolean> {
    const modalRef = this.modalService.open(ModalComponent, { size });
    modalRef.componentInstance.modal = modal;
    return modalRef.result;
  }
}
