import {
  Component,
  ElementRef,
  inject,
  OnDestroy,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import { NgbToastModule } from '@ng-bootstrap/ng-bootstrap';
import { CommonModule, NgTemplateOutlet } from '@angular/common';
import { ToastService } from '@shared/services/toast/toast.service';

@Component({
  selector: 'app-toast',
  standalone: true,
  imports: [NgbToastModule, NgTemplateOutlet, CommonModule],
  templateUrl: './toast.component.html',
  styleUrl: './toast.component.scss',
})
export class ToastComponent implements OnDestroy {
  toastService = inject(ToastService);

  isTemplate(toast: any) {
    return toast.textOrTpl instanceof TemplateRef;
  }

  ngOnDestroy(): void {
    this.toastService.clear();
  }
}
