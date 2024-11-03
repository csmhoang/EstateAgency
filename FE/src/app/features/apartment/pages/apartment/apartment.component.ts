import { Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { RouterLink } from '@angular/router';
import { FooterComponent } from '@core/layout/footer/footer.component';
import { HeaderComponent } from '@core/layout/header/header.component';
import { ApartmentFilterComponent } from '@features/apartment/components/apartment-filter/apartment-filter.component';
import { ApartmentService } from '@features/apartment/services/apartment.service';
import { RoomService } from '@features/apartment/services/room.service';
import { PostRootListComponent } from '@features/post/components/post-root-list/post-root-list.component';
import { Post } from '@features/post/models/post.model';
import { AutoCompleteComponent } from '@shared/components/form/auto-complete/auto-complete.component';
import { MiniLoadComponent } from '@shared/components/mini-load/mini-load.component';
import { PaginationComponent } from '@shared/components/pagination/pagination.component';
import { PaginationParams } from '@shared/models/pagination-params.model';
import { catchError, lastValueFrom, of } from 'rxjs';

@Component({
  selector: 'app-apartment',
  standalone: true,
  imports: [
    HeaderComponent,
    FooterComponent,
    AutoCompleteComponent,
    ApartmentFilterComponent,
    PostRootListComponent,
    RouterLink,
    PaginationComponent,
    MiniLoadComponent,
  ],
  templateUrl: './apartment.component.html',
  styleUrl: './apartment.component.scss',
})
export class ApartmentComponent implements OnInit {
  destroyRef = inject(DestroyRef);
  posts = signal<Post[]>([]);
  paginationParams = signal<PaginationParams>({
    pageSize: 0,
    count: 0,
    pageIndex: 1,
  });

  constructor(private apartmentService: ApartmentService) {}

  async ngOnInit() {
    await this.init();
  }

  async init() {
    await lastValueFrom(
      this.apartmentService.loadData(true).pipe(
        takeUntilDestroyed(this.destroyRef),
        catchError(() => of(null))
      )
    );

    const page = this.apartmentService.page();
    if (page) {
      this.paginationParams.set(page);
      this.posts.set(page.data);
    }
  }

  async onPageChange(pageIndex: number) {
    this.apartmentService.specParams.update((value) => ({
      ...value,
      pageIndex: pageIndex,
    }));
    await this.init();
  }
}
