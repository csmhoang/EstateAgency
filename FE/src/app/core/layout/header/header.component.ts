import { CommonModule, DOCUMENT } from '@angular/common';
import {
  Component,
  DestroyRef,
  ElementRef,
  inject,
  PLATFORM_ID,
  Renderer2,
  ViewChild,
} from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { RouterModule } from '@angular/router';
import { IfAuthenticatedDirective } from '@core/auth/directives/if-authenticated.directive';
import { MatBadgeModule } from '@angular/material/badge';
import { MenuComponent } from '../menu/menu.component';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    RouterModule,
    IfAuthenticatedDirective,
    CommonModule,
    MatButtonModule,
    MatMenuModule,
    MatBadgeModule,
    MenuComponent,
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent {
  @ViewChild('navmenu', { read: ElementRef })
  navmenu: ElementRef | undefined;

  @ViewChild('mobileNavToggle', { static: true })
  mobileNavToggleBtn!: ElementRef;

  destroyRef = inject(DestroyRef);
  renderer = inject(Renderer2);
  el = inject(ElementRef);
  platformId = inject(PLATFORM_ID);
  document = inject(DOCUMENT);
  nav: ElementRef | undefined;

  toggleMobile() {
    const body = document.querySelector('body');

    let mobileNavToogle = () => {
      body!.classList.toggle('mobile-nav-active');
      this.mobileNavToggleBtn.nativeElement.classList.toggle('bi-list');
      this.mobileNavToggleBtn.nativeElement.classList.toggle('bi-x');
    };

    if (this.mobileNavToggleBtn) {
      mobileNavToogle();
    }

    this.el.nativeElement
      .querySelectorAll('.navmenu a')
      .forEach((nav: HTMLElement) => {
        this.renderer.listen(nav, 'click', () => {
          if (body!.classList.contains('mobile-nav-active')) {
            mobileNavToogle();
          }
        });
      });
  }
}
