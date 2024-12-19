import {
  Directive,
  effect,
  Input,
  TemplateRef,
  ViewContainerRef,
} from '@angular/core';
import { UserService } from '@core/services/user.service';

@Directive({
  selector: '[appIfCustomer]',
  standalone: true,
})
export class IfCustomerDirective<T> {
  private condition: boolean = false;
  private hasView = false;

  constructor(
    private templateRef: TemplateRef<T>,
    private userService: UserService,
    private viewContainer: ViewContainerRef
  ) {
    effect(() => {
      const isTenant = this.userService.isTenant();
      const shouldShowView =
        (isTenant && this.condition) || (!isTenant && !this.condition);

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

  @Input('appIfCustomer') set ifCustomer(condition: boolean) {
    this.condition = condition;
  }
}
