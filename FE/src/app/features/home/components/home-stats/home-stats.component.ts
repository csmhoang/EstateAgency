import { isPlatformBrowser } from '@angular/common';
import { AfterViewInit, Component, inject, PLATFORM_ID } from '@angular/core';

declare var PureCounter: any;

@Component({
  selector: 'app-home-stats',
  standalone: true,
  imports: [],
  templateUrl: './home-stats.component.html',
  styleUrl: './home-stats.component.scss',
})
export class HomeStatsComponent implements AfterViewInit {
  platformId = inject(PLATFORM_ID);

  ngAfterViewInit(): void {
    if (isPlatformBrowser(this.platformId)) {
      this.loadPureCounter();
    }
  }

  loadPureCounter(): void {
    if (typeof PureCounter !== 'undefined') {
      new PureCounter();
    }
  }
}
