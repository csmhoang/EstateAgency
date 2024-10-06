import { DOCUMENT, isPlatformBrowser } from '@angular/common';
import {
  Component,
  inject,
  OnDestroy,
  OnInit,
  PLATFORM_ID,
  Renderer2,
} from '@angular/core';

@Component({
  selector: 'app-scroll-top',
  standalone: true,
  imports: [],
  templateUrl: './scroll-top.component.html',
  styleUrl: './scroll-top.component.scss',
})
export class ScrollTopComponent implements OnInit, OnDestroy {
  platformId = inject(PLATFORM_ID);
  document = inject(DOCUMENT);

  ngOnInit(): void {
    if (isPlatformBrowser(this.platformId)) {
      // window.addEventListener('scroll', () => this.toggleScrolled());
    }
  }
  renderer = inject(Renderer2);

  toggleScrolled(): void {
    const selectBody = this.document.body;
    const selectHeader = this.document.querySelector('#header');

    if (!selectHeader) return;

    const headerClasses = selectHeader.classList;
    if (
      !headerClasses.contains('scroll-up-sticky') &&
      !headerClasses.contains('sticky-top') &&
      !headerClasses.contains('fixed-top')
    ) {
      return;
    }

    window.scrollY > 100
      ? this.renderer.addClass(selectBody, 'scrolled')
      : this.renderer.removeClass(selectBody, 'scrolled');
  }

  ngOnDestroy(): void {
    if (isPlatformBrowser(this.platformId)) {
      // window.removeEventListener('scroll', () => this.toggleScrolled());
    }
  }
}
