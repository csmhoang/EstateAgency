import { DOCUMENT, isPlatformBrowser } from '@angular/common';
import {
  AfterViewInit,
  Component,
  ElementRef,
  inject,
  NgZone,
  PLATFORM_ID,
  Renderer2,
  ViewChild,
} from '@angular/core';
import { NavigationEnd, Router, RouterLink } from '@angular/router';
import { filter } from 'rxjs';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent implements AfterViewInit{
  nav: ElementRef | undefined;

  @ViewChild('navmenu', { read: ElementRef })
  navmenu: ElementRef | undefined;

  renderer = inject(Renderer2);
  router = inject(Router);
  ngZone = inject(NgZone);
  platformId = inject(PLATFORM_ID);
  document = inject(DOCUMENT);

  constructor() {
    this.ngZone.runOutsideAngular(() => {
      this.router.events
        .pipe(filter((event) => event instanceof NavigationEnd))
        .subscribe(() => {
          this.ngZone.run(() => {
            if (this.nav) {
              this.renderer.removeClass(this.nav, 'active');
            }
            const routers: { [key: string]: number } = {
              '/': 1,
              '/apartment': 2,
              '/lessor': 3,
              '/service': 4,
              '/contact': 5,
            };
            const underline: number | undefined = routers[this.router.url];
            if (underline) {
              this.nav = this.navmenu?.nativeElement.querySelector(
                `ul > li:nth-child(${underline}) > a`
              );
              if (this.nav) {
                this.renderer.addClass(this.nav, 'active');
              }
            }
          });
        });
    });
  }

  ngAfterViewInit(): void {
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
