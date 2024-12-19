import { CommonModule } from '@angular/common';
import {
  Component,
  DestroyRef,
  inject,
  signal,
  ViewChild,
} from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { RouterLink } from '@angular/router';
import { IsAccept, Post, StatusPost } from '@features/post/models/post.model';
import { SearchComponent } from '@shared/components/form/search/search.component';
import { PaginationComponent } from '@shared/components/pagination/pagination.component';
import { PaginationParams } from '@shared/models/pagination-params.model';
import { DialogService } from '@shared/services/dialog/dialog.service';
import { ToastService } from '@shared/services/toast/toast.service';
import { MiniLoadComponent } from '@shared/components/mini-load/mini-load.component';
import { catchError, firstValueFrom, lastValueFrom, of } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { PostUpdateComponent } from '@features/post/components/post-update/post-update.component';
import { UserService } from '@core/services/user.service';
import { AdminPostService } from '@features/management/admin/services/admin-post.service';
import { PostService } from '@features/post/services/post.service';
import { PresenceService } from '@core/services/presence.service';
import { Notice } from '@features/notification/models/notification.model';

@Component({
  selector: 'app-admin-post',
  standalone: true,
  imports: [
    MatTableModule,
    MatSortModule,
    MatButtonModule,
    MatMenuModule,
    PaginationComponent,
    SearchComponent,
    CommonModule,
    RouterLink,
    MiniLoadComponent,
  ],
  templateUrl: './admin-post.component.html',
  styleUrl: './admin-post.component.scss',
})
export class AdminPostComponent {
  destroyRef = inject(DestroyRef);
  user = this.userService.currentUser;

  displayedColumns: string[] = [
    'title',
    'name',
    'availableFrom',
    'status',
    'isAccept',
    'createdAt',
    'optional',
  ];
  dataSource = new MatTableDataSource<Post>();

  paginationParams = signal<PaginationParams>({
    pageSize: 0,
    count: 0,
    pageIndex: 1,
  });
  isAcceptFilter = IsAccept;
  statusFilter = StatusPost;

  @ViewChild(MatSort) sort?: MatSort;

  constructor(
    private dialogService: DialogService,
    private toastService: ToastService,
    private adminPostService: AdminPostService,
    private userService: UserService,
    private postService: PostService,
    private presenceService: PresenceService
  ) {}

  async ngOnInit() {
    if (this.user()) {
      this.adminPostService.specParams.set({
        pageSize: 10,
        pageIndex: 1,
        isAccept: 'Pending',
        status: 'Published',
      });
      await this.init();
    }
  }

  async init() {
    await lastValueFrom(
      this.adminPostService.loadData().pipe(
        takeUntilDestroyed(this.destroyRef),
        catchError(() => of(null))
      )
    );

    const page = this.adminPostService.page();
    if (page) {
      this.paginationParams.set(page);
      this.dataSource.data = page.data;
      if (this.sort) {
        this.dataSource.sort = this.sort;
      }
    }
  }

  onUpdate(post: Post, landlordId: string) {
    this.dialogService.form(PostUpdateComponent, post).then(async () => {
      await this.presenceService.createNotification({
        receiverId: landlordId,
        title: 'Kiểm duyệt bài đăng',
        content: `Admin - ${this.user()?.fullName} đã sửa bài đăng của bạn.`,
      } as Notice);

      this.toastService.success('Cập nhật bài đăng thành công!');
      await this.init();
    });
  }

  onAccept(postId: string, landlordId: string) {
    this.dialogService
      .confirm({
        title: 'Xác nhận bài đăng',
        content: 'Bạn có chắc muốn cho phép bài đăng này không?',
        button: {
          accept: 'Xác nhận',
          decline: 'Hủy bỏ',
        },
      })
      .then(async () => {
        const response = await firstValueFrom(
          this.postService.response(postId, 'Accepted').pipe(
            takeUntilDestroyed(this.destroyRef),
            catchError(() => of(null))
          )
        );
        if (response?.success) {
          await this.presenceService.createNotification({
            receiverId: landlordId,
            title: 'Kiểm duyệt bài đăng',
            content: `Admin - ${
              this.user()?.fullName
            } đã chấp nhận bài đăng của bạn.`,
          } as Notice);
          for (let i = 0; i < this.dataSource.data.length; i++) {
            if (this.dataSource.data[i].id === postId) {
              this.dataSource.data[i].isAccept = 'Accepted';
              this.dataSource._updateChangeSubscription();
              break;
            }
          }
          this.toastService.success('Xác nhận đăng bài thành công!');
        }
      });
  }

  onReject(postId: string, landlordId: string) {
    this.dialogService
      .confirm({
        title: 'Từ chối bài đăng',
        content: 'Bạn có chắc muốn từ chối bài đăng này không?',
        button: {
          accept: 'Xác nhận',
          decline: 'Hủy bỏ',
        },
      })
      .then(async () => {
        const response = await firstValueFrom(
          this.postService.response(postId, 'Rejected').pipe(
            takeUntilDestroyed(this.destroyRef),
            catchError(() => of(null))
          )
        );
        if (response?.success) {
          await this.presenceService.createNotification({
            receiverId: landlordId,
            title: 'Kiểm duyệt bài đăng',
            content: `Admin - ${
              this.user()?.fullName
            } đã Từ chối bài đăng của bạn.`,
          } as Notice);
          for (let i = 0; i < this.dataSource.data.length; i++) {
            if (this.dataSource.data[i].id === postId) {
              this.dataSource.data[i].isAccept = 'Rejected';
              this.dataSource._updateChangeSubscription();
              break;
            }
          }
          this.toastService.success('Xác nhận từ chối thành công!');
        }
      });
  }

  async onSearch(query: string) {
    this.adminPostService.specParams.update((value) => ({
      ...value,
      search: query,
      pageIndex: 1,
    }));
    await this.init();
  }

  async onPageChange(pageIndex: number) {
    this.adminPostService.specParams.update((value) => ({
      ...value,
      pageIndex: pageIndex,
    }));
    await this.init();
  }
}
