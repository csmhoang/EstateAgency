import { DOCUMENT, isPlatformBrowser } from '@angular/common';
import {
  AfterViewInit,
  Component,
  inject,
  PLATFORM_ID,
  Renderer2,
} from '@angular/core';

@Component({
  selector: 'app-faq',
  standalone: true,
  imports: [],
  templateUrl: './faq.component.html',
  styleUrl: './faq.component.scss',
})
export class FAQComponent implements AfterViewInit {
  platformId = inject(PLATFORM_ID);
  document = inject(DOCUMENT);
  renderer = inject(Renderer2);

  ngAfterViewInit(): void {
    if (isPlatformBrowser(this.platformId)) {
      this.document
        .querySelectorAll<HTMLElement>('.faq-item h3, .faq-item .faq-toggle')
        .forEach((faqItem) => {
          this.renderer.listen(
            faqItem,
            'click',
            this.toggleQuestions.bind(faqItem)
          );
        });
    }
  }

  toggleQuestions(this: HTMLElement) {
    const parent = this.parentElement as HTMLElement;
    parent.classList.toggle('faq-active');
  }
}
