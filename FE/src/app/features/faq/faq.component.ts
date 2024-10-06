import { DOCUMENT, isPlatformBrowser } from '@angular/common';
import { AfterViewInit, Component, inject, PLATFORM_ID } from '@angular/core';

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

  ngAfterViewInit(): void {
    if (isPlatformBrowser(this.platformId)) {
      this.toggleQuestions();
    }
  }
  toggleQuestions() {
    this.document
      .querySelectorAll<HTMLElement>('.faq-item h3, .faq-item .faq-toggle')
      .forEach((faqItem) => {
        faqItem.addEventListener('click', () => {
          const parent = faqItem.parentNode as HTMLElement;
          parent.classList.toggle('faq-active');
        });
      });
  }
}
