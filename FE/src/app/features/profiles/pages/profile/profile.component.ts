import { Breakpoints, BreakpointObserver } from '@angular/cdk/layout';
import { CommonModule } from '@angular/common';
import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatRippleModule } from '@angular/material/core';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { RouterModule, RouterOutlet } from '@angular/router';
import { FooterComponent } from '@core/layout/footer/footer.component';
import { HeaderComponent } from '@core/layout/header/header.component';
import { UserService } from '@core/services/user.service';
import { ProfileService } from '@features/profiles/services/profile.service';
import { ToastService } from '@shared/services/toast/toast.service';
import { Observable, catchError, map, of, shareReplay } from 'rxjs';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [
    HeaderComponent,
    FooterComponent,
    MatSidenavModule,
    MatToolbarModule,
    MatRippleModule,
    CommonModule,
    RouterModule,
    RouterOutlet,
  ],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss',
})
export class ProfileComponent {
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
    private profileService: ProfileService
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
}
