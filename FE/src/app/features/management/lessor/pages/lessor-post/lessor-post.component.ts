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
import { LessorPostService } from '../../services/lessor-post.service';
import { MiniLoadComponent } from '@shared/components/mini-load/mini-load.component';
import { catchError, firstValueFrom, lastValueFrom, of } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { PostUpdateComponent } from '@features/post/components/post-update/post-update.component';

@Component({
  selector: 'app-lessor-post',
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
  templateUrl: './lessor-post.component.html',
  styleUrl: './lessor-post.component.scss',
})
export class LessorPostComponent {
  destroyRef = inject(DestroyRef);

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
    private lessorPostService: LessorPostService
  ) {}
  async ngOnInit() {
    this.lessorPostService.specParams.set({ pageSize: 10, pageIndex: 1 });
    await this.init();
  }

  async init() {
    await lastValueFrom(
      this.lessorPostService.loadData(true).pipe(
        takeUntilDestroyed(this.destroyRef),
        catchError(() => of(null))
      )
    );

    const page = this.lessorPostService.page();
    if (page) {
      this.paginationParams.set(page);
      this.dataSource.data = page.data;
      if (this.sort) {
        this.dataSource.sort = this.sort;
      }
    }
  }

  onUpdate(post: Post) {
    this.dialogService.form(PostUpdateComponent, post).then(async () => {
      this.toastService.success('Cập nhật bài đăng thành công!');
      await this.init();
    });
  }

  onDelete(postId: string) {
    this.dialogService
      .confirm({
        title: 'Xác nhận gỡ bài đăng',
        content: 'Bạn có chắc muốn gỡ bài đăng này không?',
        button: {
          accept: 'Gỡ',
          decline: 'Hủy bỏ',
        },
      })
      .then(async () => {
        const response = await firstValueFrom(
          this.lessorPostService.remove(postId).pipe(
            takeUntilDestroyed(this.destroyRef),
            catchError(() => of(null))
          )
        );
        if (response?.success) {
          this.toastService.success('Xóa bản phòng thành công!');
          await this.init();
        }
      });
  }

  async onSearch(query: string) {
    this.lessorPostService.specParams.update((value) => ({
      ...value,
      search: query,
      pageIndex: 1,
    }));
    await this.init();
  }

  async onPageChange(pageIndex: number) {
    this.lessorPostService.specParams.update((value) => ({
      ...value,
      pageIndex: pageIndex,
    }));
    await this.init();
  }
}
