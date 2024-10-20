import { Injectable, TemplateRef } from '@angular/core';
import { Toast } from '@shared/models/toast.model';

@Injectable({
  providedIn: 'root',
})
export class ToastService {
  toasts: Toast[] = [];

  show(
    content: string | TemplateRef<any>,
    options: { class: string; delay?: number } = { class: '' }
  ) {
    return this.toasts.push({ content, ...options });
  }

  remove(toast: Toast) {
    this.toasts = this.toasts.filter((t) => t !== toast);
  }

  clear() {
    this.toasts.splice(0, this.toasts.length);
  }

  success(content: string) {
    this.show(content, { class: 'bg-success text-light bg-opacity-75', delay: 5000 });
  }

  error(content: string) {
    this.show(content, { class: 'bg-danger text-light bg-opacity-75', delay: 5000 });
  }

  warn(content: string) {
    this.show(content, { class: 'bg-warning text-light bg-opacity-75', delay: 5000 });
  }

  template(content: TemplateRef<any>) {
    this.show(content, { class: 'bg-light text-light bg-opacity-75', delay: 5000 });
  }
}
