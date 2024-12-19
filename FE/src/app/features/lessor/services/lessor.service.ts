import { HttpClient, HttpContext } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { SkipPreloader } from '@core/interceptors/skip.resolver';
import { PageData } from '@core/models/page-data.model';
import { Result } from '@core/models/result.model';
import { SpecUserParams } from '@core/models/spec-user-params.model';
import { User } from '@core/models/user.model';
import { UserService } from '@core/services/user.service';
import { map, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LessorService {
  pageSignal = signal<PageData<User[]> | null>(null);
  public page = this.pageSignal.asReadonly();
  public specUserParams = signal<SpecUserParams>({});

  constructor(private http: HttpClient, private userService: UserService) {}

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

  getSearchOptions() {
    return this.http
      .get<Result<string[]>>('/users/search-options', {
        context: new HttpContext().set(SkipPreloader, true),
      })
      .pipe(map((response) => response.data));
  }
}
