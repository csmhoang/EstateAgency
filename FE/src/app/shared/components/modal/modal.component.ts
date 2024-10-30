import { CommonModule, NgTemplateOutlet } from '@angular/common';
import {
  Component,
  inject,
  Input,
  TemplateRef,
} from '@angular/core';
import { NgbActiveModal, NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { Modal } from '@shared/models/modal.model';

@Component({
  selector: 'app-modal',
  standalone: true,
  imports: [NgbModalModule, CommonModule, NgTemplateOutlet],
  templateUrl: './modal.component.html',
  styleUrl: './modal.component.scss',
})
export class ModalComponent {
  activeModal = inject(NgbActiveModal);

  @Input({ required: true })
  modal?: Modal;

  isTemplate(modal?: Modal) {
    return modal?.content instanceof TemplateRef;
  }

  accept() {
    this.activeModal.close(true);
  }

  decline() {
    this.activeModal.dismiss(false);
  }
}
