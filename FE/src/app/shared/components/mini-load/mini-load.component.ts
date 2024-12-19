import { Component, inject } from '@angular/core';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MiniLoadService } from '@shared/services/mini-load/mini-load.service';

@Component({
  selector: 'app-mini-load',
  standalone: true,
  imports: [MatProgressSpinnerModule],
  templateUrl: './mini-load.component.html',
  styleUrl: './mini-load.component.scss',
})
export class MiniLoadComponent {
  loading = inject(MiniLoadService).loading;
}
