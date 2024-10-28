import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MiniLoadService {
  #loading = signal(false);
  loading = this.#loading.asReadonly();

  loadingOn() {
    this.#loading.set(true);
  }

  loadingOff() {
    this.#loading.set(false);
  }
}
