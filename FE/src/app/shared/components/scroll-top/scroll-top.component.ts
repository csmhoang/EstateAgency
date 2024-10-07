import { DOCUMENT, isPlatformBrowser } from '@angular/common';
import {
  AfterViewInit,
  Component,
  inject,
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
export class ScrollTopComponent implements AfterViewInit {
  platformId = inject(PLATFORM_ID);
  document = inject(DOCUMENT);
  renderer = inject(Renderer2);

  ngAfterViewInit(): void {
    if (isPlatformBrowser(this.platformId)) {
      this.renderer.listen(this.document, 'scroll', () => {
        this.toggleScrolled();
        this.toggleScrollTop();
      });
    }
  }

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

  toggleScrollTop() {
    const scrollTop = this.document.querySelector('.scroll-top');

    if (scrollTop) {
      this.renderer.listen(scrollTop, 'click', (e: Event) => {
        e.preventDefault();
        window.scrollTo({
          top: 0,
          behavior: 'smooth',
        });
      });

      window.scrollY > 100
        ? scrollTop.classList.add('active')
        : scrollTop.classList.remove('active');
    }
  }
}
