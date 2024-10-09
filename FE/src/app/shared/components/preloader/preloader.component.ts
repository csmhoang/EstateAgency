import { Component, inject } from '@angular/core';
import { PreloaderService } from './preloader.service';

@Component({
  selector: 'app-preloader',
  standalone: true,
  imports: [],
  templateUrl: './preloader.component.html',
  styleUrl: './preloader.component.scss',
})
export class PreloaderComponent {
  loading = inject(PreloaderService).loading;
}
