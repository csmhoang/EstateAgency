import { inject, Injectable } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmComponent } from '@shared/components/dialog/confirm/confirm.component';
import { Confirm } from '@shared/components/dialog/confirm/confirm.model';

@Injectable({
  providedIn: 'root',
})
export class DialogService {
  modalService = inject(NgbModal);

  confirm(confirm: Confirm): Promise<boolean> {
    const modalRef = this.modalService.open(ConfirmComponent);
    modalRef.componentInstance.confirm = confirm;
    return modalRef.result;
  }
}
