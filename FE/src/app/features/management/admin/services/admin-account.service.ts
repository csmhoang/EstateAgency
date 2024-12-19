import { Injectable, signal } from '@angular/core';
import { PageData } from '@core/models/page-data.model';
import { SpecUserParams } from '@core/models/spec-user-params.model';
import { User } from '@core/models/user.model';
import { UserService } from '@core/services/user.service';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AdminAccountService {
  pageSignal = signal<PageData<User[]> | null>(null);
  public page = this.pageSignal.asReadonly();
  public specUserParams = signal<SpecUserParams>({});

  constructor(private userService: UserService) {}

  loadData(isDisplayMiniLoading: boolean = true) {
    return this.userService
      .getList(this.specUserParams(), isDisplayMiniLoading)
      .pipe(
        tap({
          next: (page) => {
            if (page) {
              this.pageSignal.set(page);
            }
          },
        })
      );
  }
}
