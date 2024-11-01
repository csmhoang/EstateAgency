import { NgTemplateOutlet } from '@angular/common';
import { Component, inject, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Confirm } from '@shared/models/modal.model';

@Component({
  selector: 'app-confirm-modal',
  standalone: true,
  imports: [],
  templateUrl: './confirm-modal.component.html',
  styleUrl: './confirm-modal.component.scss',
})
export class ConfirmModalComponent {
  activeModal = inject(NgbActiveModal);
  @Input({ required: true })
  modal!: Confirm;

  accept() {
    this.activeModal.close(true);
  }

  decline() {
    this.activeModal.dismiss(false);
  }
}
