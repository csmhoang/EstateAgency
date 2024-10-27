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
import { distinctUntilChanged } from 'rxjs';

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

  private condition: boolean = false;
  private hasView = false;

  ngOnInit(): void {
    this.userService.isAuthenticated
      .pipe(takeUntilDestroyed(this.destroyRef), distinctUntilChanged())
      .subscribe((isAuthenticated: boolean) => {
        const shouldShowView =
          (isAuthenticated && this.condition) ||
          (!isAuthenticated && !this.condition);
          
        if (shouldShowView && !this.hasView) {
          // Hiển thị view nếu điều kiện khớp và view chưa được tạo
          this.viewContainer.createEmbeddedView(this.templateRef);
          this.hasView = true;
        } else if (!shouldShowView && this.hasView) {
          // Xóa view nếu điều kiện không khớp và view đã tồn tại
          this.viewContainer.clear();
          this.hasView = false;
        } else if (!shouldShowView && !this.hasView) {
          // Trường hợp này đảm bảo view sẽ không xuất hiện
          this.viewContainer.createEmbeddedView(this.templateRef);
          this.viewContainer.clear();
        }
      });
  }

  @Input('appIfAuthenticated') set ifAuthenticated(condition: boolean) {
    this.condition = condition;
  }
}
