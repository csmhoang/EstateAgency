import {
  CommonModule,
  DOCUMENT,
  isPlatformBrowser,
  NgIf,
} from '@angular/common';
import {
  AfterViewInit,
  Component,
  DestroyRef,
  ElementRef,
  inject,
  Input,
  PLATFORM_ID,
  Renderer2,
  ViewChild,
} from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { RouterModule } from '@angular/router';
import { IfAuthenticatedDirective } from '@core/auth/directives/if-authenticated.directive';
import { AuthService } from '@core/auth/services/auth.service';
import { UserService } from '@core/services/user.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    NgIf,
    RouterModule,
    IfAuthenticatedDirective,
    CommonModule,
    MatButtonModule,
    MatMenuModule,
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent implements AfterViewInit {
  @ViewChild('navmenu', { read: ElementRef })
  navmenu: ElementRef | undefined;

  destroyRef = inject(DestroyRef);
  renderer = inject(Renderer2);
  platformId = inject(PLATFORM_ID);
  document = inject(DOCUMENT);

  constructor(private authService: AuthService) {}

  nav: ElementRef | undefined;

  user$ = inject(UserService).currentUser.pipe(
    takeUntilDestroyed(this.destroyRef)
  );

  ngAfterViewInit(): void {
    if (isPlatformBrowser(this.platformId)) {
      this.toggleMobile();
    }
  }

  onLogout() {
    this.authService.logout();
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
