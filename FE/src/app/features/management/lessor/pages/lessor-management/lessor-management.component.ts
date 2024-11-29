import { Component } from '@angular/core';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatRippleModule } from '@angular/material/core';
import { MatMenuModule } from '@angular/material/menu';

import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable, map, shareReplay } from 'rxjs';
import { CommonModule } from '@angular/common';
import { RouterModule, RouterOutlet } from '@angular/router';
import { AuthService } from '@core/auth/services/auth.service';
import { UserService } from '@core/services/user.service';
@Component({
  selector: 'app-lessor-management',
  standalone: true,
  imports: [
    MatSidenavModule,
    MatToolbarModule,
    MatButtonModule,
    MatRippleModule,
    MatMenuModule,
    CommonModule,
    RouterModule,
    RouterOutlet,
  ],
  templateUrl: './lessor-management.component.html',
  styleUrl: './lessor-management.component.scss',
})
export class LessorManagementComponent {
  isHandset$: Observable<boolean> = this.breakpointObserver
    .observe(Breakpoints.Handset)
    .pipe(
      map((result) => result.matches),
      shareReplay()
    );
  user = this.userService.currentUser();

  constructor(
    private breakpointObserver: BreakpointObserver,
    private authService: AuthService,
    private userService: UserService
  ) {}

  onLogout() {
    this.authService.logout();
  }
}
