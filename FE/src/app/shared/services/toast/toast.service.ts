import { Injectable, TemplateRef } from '@angular/core';
import { Toast } from '@shared/components/toast/toast.model';

@Injectable({
  providedIn: 'root',
})
export class ToastService {
  toasts: Toast[] = [];

  show(
    textOrTpl: string | TemplateRef<any>,
    options: { class: string; delay?: number } = { class: '' }
  ) {
    return this.toasts.push({ textOrTpl, ...options });
  }

  remove(toast: Toast) {
    this.toasts = this.toasts.filter((t) => t !== toast);
  }

  clear() {
    this.toasts.splice(0, this.toasts.length);
  }

  success(message: string) {
    this.show(message, { class: 'bg-success text-light', delay: 5000 });
  }

  error(message: string) {
    this.show(message, { class: 'bg-danger text-light', delay: 5000 });
  }

  warn(message: string) {
    this.show(message, { class: 'bg-warning text-light', delay: 5000 });
  }

}
