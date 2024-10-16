import {
  DestroyRef,
  Directive,
  inject,
  Input,
  OnInit,
  TemplateRef,
  ViewContainerRef,
} from '@angular/core';
import { UserService } from '@core/services/user.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { User } from '@core/models/user.model';

@Directive({
  selector: '[appIfAuthenticated]',
  standalone: true,
})
export class IfAuthenticatedDirective<T> implements OnInit {
  destroyRef = inject(DestroyRef);
  constructor(
    private templateRef: TemplateRef<T>,
    private userService: UserService,
    private viewContainer: ViewContainerRef
  ) {}

  condition: boolean = false;
  hasView = false;
  currentUser: User | null = null;

  ngOnInit(): void {
    this.userService.isAuthenticated
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((isAuthenticated: boolean) => {
        const authRequired = isAuthenticated && this.condition;
        const unauthRequired = !isAuthenticated && !this.condition;
        if ((authRequired || unauthRequired) && !this.hasView) {
          this.viewContainer.createEmbeddedView(this.templateRef);
          this.hasView = true;
        } else if (this.hasView) {
          this.viewContainer.clear();
          this.hasView = false;
        }
      });
  }

  @Input('appIfAuthenticated') set ifAuthenticated(condition: boolean) {
    this.condition = condition;
  }
}
