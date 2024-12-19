import { Component, DestroyRef, inject, Input } from '@angular/core';
import { FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { catchError, of } from 'rxjs';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { AuthService } from '@core/auth/services/auth.service';

@Component({
  selector: 'app-block-account',
  standalone: true,
  imports: [MatFormFieldModule, MatInputModule, ReactiveFormsModule],
  templateUrl: './block-account.component.html',
  styleUrl: './block-account.component.scss',
})
export class BlockAccountComponent {
  @Input() data!: string;
  destroyRef = inject(DestroyRef);
  activeModal = inject(NgbActiveModal);

  duration = new FormControl('', [Validators.required]);

  constructor(private authService: AuthService) {}

  onBlock() {
    if (this.duration.valid) {
      this.authService
        .blockUser(this.data, parseInt(this.duration.value!))
        .pipe(
          takeUntilDestroyed(this.destroyRef),
          catchError(() => of(null))
        )
        .subscribe((response) => {
          if (response?.success) {
            this.activeModal.close(true);
          }
        });
    }
  }

  accept() {
    this.onBlock();
  }

  decline() {
    this.activeModal.dismiss(false);
  }

  errorForDuration(): string {
    if (this.duration?.hasError('required')) {
      return 'Thời gian khóa không được để trống!';
    }
    return '';
  }
}
