import { Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { DashboardService } from '@features/management/lessor/services/dashboard.service';
import { catchError, of } from 'rxjs';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './admin-dashboard.component.html',
  styleUrl: './admin-dashboard.component.scss',
})
export class AdminDashboardComponent implements OnInit {
  destroyRef = inject(DestroyRef);

  numberOfMonth = new FormControl(1);
  visitCount = signal<number>(0);

  constructor(private dashboardService: DashboardService) {}

  ngOnInit() {
    this.onVisitCount();
  }

  onVisitCount() {
    if (this.numberOfMonth.value) {
      this.dashboardService
        .visitCount(this.numberOfMonth.value ?? 1)
        .pipe(
          takeUntilDestroyed(this.destroyRef),
          catchError(() => of(null))
        )
        .subscribe((data) => {
          if (data) this.visitCount.set(data);
        });
    }
  }
}
