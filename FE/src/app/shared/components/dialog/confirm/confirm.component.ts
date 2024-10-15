import { Component, inject, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Confirm } from './confirm.model';

@Component({
  selector: 'app-confirm',
  standalone: true,
  imports: [],
  templateUrl: './confirm.component.html',
  styleUrl: './confirm.component.scss',
})
export class ConfirmComponent {
  activeModal = inject(NgbActiveModal);

  @Input({ required: true })
  confirm!: Confirm;

  accept() {
    this.activeModal.close(true);
  }

  decline() {
    this.activeModal.dismiss(false);
  }
}
