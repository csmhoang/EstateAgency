import { Breakpoints, BreakpointObserver } from '@angular/cdk/layout';
import { CommonModule } from '@angular/common';
import { Component, DestroyRef, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatButtonModule } from '@angular/material/button';
import { MatRippleModule } from '@angular/material/core';
import { MatMenuModule } from '@angular/material/menu';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { RouterModule, RouterOutlet } from '@angular/router';
import { IfAuthenticatedDirective } from '@core/auth/directives/if-authenticated.directive';
import { AuthService } from '@core/auth/services/auth.service';
import { UserService } from '@core/services/user.service';
import { ToastService } from '@shared/services/toast/toast.service';
import { Observable, catchError, map, of, shareReplay } from 'rxjs';
import { MenuComponent } from "../../../../../core/layout/menu/menu.component";

@Component({
  selector: 'app-lessor-profile',
  standalone: true,
  imports: [
    MatMenuModule,
    MatSidenavModule,
    MatToolbarModule,
    MatRippleModule,
    CommonModule,
    RouterModule,
    RouterOutlet,
    MatButtonModule,
    IfAuthenticatedDirective,
    MenuComponent
],
  templateUrl: './lessor-profile.component.html',
  styleUrl: './lessor-profile.component.scss',
})
export class LessorProfileComponent {
  destroyRef = inject(DestroyRef);
  user = this.userService.currentUser();
  isHandset$: Observable<boolean> = this.breakpointObserver
    .observe(Breakpoints.Handset)
    .pipe(
      map((result) => result.matches),
      shareReplay()
    );

  constructor(
    private breakpointObserver: BreakpointObserver,
    private userService: UserService,
    private toastService: ToastService,
    private authService: AuthService,
  ) {}

  setAvatar(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      const file = input.files[0];
      if (this.user) {
        this.userService
          .avatar(this.user.id, file)
          .pipe(
            takeUntilDestroyed(this.destroyRef),
            catchError(() => of(null))
          )
          .subscribe((response) => {
            if (response?.success) {
              this.toastService.success('Cập nhật avatar thành công!');
              this.userService
                .init(true)
                .pipe(
                  takeUntilDestroyed(this.destroyRef),
                  catchError(() => of(null))
                )
                .subscribe();
            }
          });
      }
    }
  }

  onLogout() {
    this.authService.logout();
  }
}
