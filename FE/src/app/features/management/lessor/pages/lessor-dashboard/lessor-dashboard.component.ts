import { Component, DestroyRef, inject } from '@angular/core';
import { DashboardService } from '../../services/dashboard.service';
import { CommonModule } from '@angular/common';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { catchError, of } from 'rxjs';

@Component({
  selector: 'app-lessor-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './lessor-dashboard.component.html',
  styleUrl: './lessor-dashboard.component.scss',
})
export class LessorDashboardComponent   {
  destroyRef = inject(DestroyRef);

  roomCount = this.dashboardService.roomCount().pipe(
    takeUntilDestroyed(this.destroyRef),
    catchError(() => of(null))
  );

  roomBlankCount = this.dashboardService.roomBlankCount().pipe(
    takeUntilDestroyed(this.destroyRef),
    catchError(() => of(null))
  );

  tenantCount =  this.dashboardService.tenantCount().pipe(
    takeUntilDestroyed(this.destroyRef),
    catchError(() => of(null))
  )

  revenue = this.dashboardService.revenue().pipe(
    takeUntilDestroyed(this.destroyRef),
    catchError(() => of(null))
  )

  constructor(
    private dashboardService: DashboardService
  ) {}
}
