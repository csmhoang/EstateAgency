import { DOCUMENT, isPlatformBrowser } from '@angular/common';
import {
  AfterViewInit,
  Component,
  ElementRef,
  inject,
  Input,
  PLATFORM_ID,
  Renderer2,
  ViewChild,
} from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent implements AfterViewInit {
  @Input()
  underline?: number;

  nav: ElementRef | undefined;

  @ViewChild('navmenu', { read: ElementRef })
  navmenu: ElementRef | undefined;

  renderer = inject(Renderer2);
  platformId = inject(PLATFORM_ID);
  document = inject(DOCUMENT);

  ngAfterViewInit(): void {
    if (this.underline) {
      const router = this.navmenu!.nativeElement.querySelector(
        `ul > li:nth-child(${this.underline}) > a`
      );
      if (router) {
        this.renderer.addClass(router, 'active');
      }
    }

    if (isPlatformBrowser(this.platformId)) {
      this.toggleMobile();
    }
  }

  toggleMobile() {
    const mobileNavToggleBtn =
      this.document.querySelector('.mobile-nav-toggle');
    const body = this.document.querySelector('body');

    let mobileNavToogle = () => {
      body!.classList.toggle('mobile-nav-active');
      mobileNavToggleBtn!.classList.toggle('bi-list');
      mobileNavToggleBtn!.classList.toggle('bi-x');
    };

    if (mobileNavToggleBtn) {
      this.renderer.listen(mobileNavToggleBtn, 'click', mobileNavToogle);
    }

    this.document.querySelectorAll('.navmenu a').forEach((nav) => {
      this.renderer.listen(nav, 'click', () => {
        if (this.document.querySelector('.mobile-nav-active')) {
          mobileNavToogle();
        }
      });
    });
  }
}
