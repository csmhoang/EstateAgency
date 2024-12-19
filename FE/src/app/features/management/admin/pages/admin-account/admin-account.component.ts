import { CommonModule } from '@angular/common';
import {
  Component,
  DestroyRef,
  inject,
  OnInit,
  signal,
  ViewChild,
} from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { SearchComponent } from '@shared/components/form/search/search.component';
import { PaginationComponent } from '@shared/components/pagination/pagination.component';
import { DialogService } from '@shared/services/dialog/dialog.service';
import { PaginationParams } from '@shared/models/pagination-params.model';
import { MiniLoadComponent } from '@shared/components/mini-load/mini-load.component';
import { ToastService } from '@shared/services/toast/toast.service';
import { catchError, firstValueFrom, lastValueFrom, of } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { UserService } from '@core/services/user.service';
import { Gender, User } from '@core/models/user.model';
import { AdminAccountService } from '../../services/admin-account.service';
import { BlockAccountComponent } from '../../components/block-account/block-account.component';
import { AuthService } from '@core/auth/services/auth.service';
import { MessengerComponent } from '@features/messenger/components/messenger/messenger.component';

@Component({
  selector: 'app-admin-account',
  standalone: true,
  imports: [
    MatTableModule,
    MatSortModule,
    MatButtonModule,
    MatMenuModule,
    PaginationComponent,
    SearchComponent,
    CommonModule,
    MiniLoadComponent,
  ],
  templateUrl: './admin-account.component.html',
  styleUrl: './admin-account.component.scss',
})
export class AdminAccountComponent implements OnInit {
  destroyRef = inject(DestroyRef);
  user = this.userService.currentUser();
  displayedColumns: string[] = [
    'avatarUrl',
    'fullname',
    'gender',
    'dateOfBirth',
    'email',
    'address',
    'createdAt',
    'lockoutEnd',
    'optional',
  ];
  dataSource = new MatTableDataSource<User>();
  paginationParams = signal<PaginationParams>({
    pageSize: 0,
    count: 0,
    pageIndex: 1,
  });

  GenderFilter = Gender;

  @ViewChild(MatSort) sort?: MatSort;

  constructor(
    private dialogService: DialogService,
    private toastService: ToastService,
    private adminAccountService: AdminAccountService,
    private userService: UserService,
    private authService: AuthService
  ) {}

  async ngOnInit() {
    if (this.user) {
      this.adminAccountService.specUserParams.set({
        pageSize: 10,
        pageIndex: 1,
      });
      await this.init();
    }
  }

  async init() {
    await lastValueFrom(
      this.adminAccountService.loadData().pipe(
        takeUntilDestroyed(this.destroyRef),
        catchError(() => of(null))
      )
    );

    const page = this.adminAccountService.page();
    if (page) {
      this.paginationParams.set(page);
      this.dataSource.data = page.data;
      if (this.sort) {
        this.dataSource.sort = this.sort;
      }
    }
  }

  async onPageChange(pageIndex: number) {
    this.adminAccountService.specUserParams.update((value) => ({
      ...value,
      pageIndex: pageIndex,
    }));
    await this.init();
  }

  async onSearch(query: string) {
    this.adminAccountService.specUserParams.update((value) => ({
      ...value,
      search: query,
      pageIndex: 1,
    }));
    await this.init();
  }

  countDownFilter(time: Date) {
    const now = new Date();
    const seconds = Math.floor(
      (new Date(time).getTime() - now.getTime()) / 1000
    );

    const intervals: { [key: string]: number } = {
      ngày: 86400,
      giờ: 3600,
      phút: 60,
      giây: 1,
    };

    for (const [unit, value] of Object.entries(intervals)) {
      const interval = Math.floor(seconds / value);
      if (interval >= 1) {
        return `${interval} ${unit}`;
      }
    }
    return 'Đã mở khóa';
  }

  isBlock(lockoutEnd?: Date) {
    if (lockoutEnd) {
      return new Date(lockoutEnd) >= new Date();
    }
    return false;
  }

  onBlock(userId: string) {
    this.dialogService
      .form(BlockAccountComponent, userId, 'sm')
      .then(async () => {
        this.toastService.success('Khóa tài khoản thành công!');
        await this.init();
      });
  }

  onUnBlock(userId: string) {
    this.dialogService
      .confirm({
        title: 'Xác nhận mở khóa tài khoản',
        content: 'Bạn có chắc muốn mở khóa tài khoản này không?',
        button: {
          accept: 'Xác nhận',
          decline: 'Hủy bỏ',
        },
      })
      .then(async () => {
        const response = await firstValueFrom(
          this.authService.unBlockUser(userId).pipe(
            takeUntilDestroyed(this.destroyRef),
            catchError(() => of(null))
          )
        );
        if (response?.success) {
          for (let i = 0; i < this.dataSource.data.length; i++) {
            if (this.dataSource.data[i].id === userId) {
              this.dataSource.data[i].lockoutEnd = undefined;
              this.dataSource._updateChangeSubscription();
              break;
            }
          }
          this.toastService.success('Mở khóa tài khoản thành công!');
        }
      });
  }
  onChat(otherId?: string) {
    this.dialogService.form(MessengerComponent, otherId);
  }
}
