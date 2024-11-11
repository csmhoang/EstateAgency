import { CommonModule } from '@angular/common';
import {
  Component,
  DestroyRef,
  inject,
  OnInit,
  signal,
  ViewChild,
} from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { RouterLink } from '@angular/router';
import { FooterComponent } from '@core/layout/footer/footer.component';
import { HeaderComponent } from '@core/layout/header/header.component';
import { ApartmentService } from '@features/apartment/services/apartment.service';
import { RoomService } from '@features/apartment/services/room.service';
import { PostRootListComponent } from '@features/post/components/post-root-list/post-root-list.component';
import { Post } from '@features/post/models/post.model';
import { SpecPostParams } from '@features/post/models/spec-post-params.model';
import { PostService } from '@features/post/services/post.service';
import { NgbTypeaheadModule, NgbTypeahead } from '@ng-bootstrap/ng-bootstrap';
import { MiniLoadComponent } from '@shared/components/mini-load/mini-load.component';
import { PaginationComponent } from '@shared/components/pagination/pagination.component';
import { PaginationParams } from '@shared/models/pagination-params.model';
import {
  catchError,
  debounceTime,
  distinctUntilChanged,
  firstValueFrom,
  lastValueFrom,
  map,
  Observable,
  of,
  OperatorFunction,
} from 'rxjs';

@Component({
  selector: 'app-apartment',
  standalone: true,
  imports: [
    HeaderComponent,
    FooterComponent,
    PostRootListComponent,
    RouterLink,
    PaginationComponent,
    MiniLoadComponent,
    CommonModule,
    ReactiveFormsModule,
    NgbTypeaheadModule,
    FormsModule,
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

  options: string[] = [];

  provices$ = firstValueFrom(
    this.roomService.getProvince().pipe(
      takeUntilDestroyed(this.destroyRef),
      catchError(() => of(null))
    )
  );

  select: OperatorFunction<string, readonly string[]> = (
    text$: Observable<string>
  ) =>
    text$.pipe(
      debounceTime(200),
      distinctUntilChanged(),
      map((term) =>
        (term === ''
          ? this.options
          : this.options.filter(
              (v) => v.toLowerCase().indexOf(term.toLowerCase()) > -1
            )
        ).slice(0, 10)
      )
    );

  @ViewChild('instance', { static: true }) instance!: NgbTypeahead;

  form: FormGroup = new FormGroup({});

  province?: AbstractControl | null;
  search?: AbstractControl | null;
  category?: AbstractControl | null;
  price?: AbstractControl | null;
  area?: AbstractControl | null;
  new?: AbstractControl | null;
  favorite?: AbstractControl | null;
  priceSort?: AbstractControl | null;
  areaSort?: AbstractControl | null;

  constructor(
    private formBuilder: FormBuilder,
    private apartmentService: ApartmentService,
    private roomService: RoomService,
    private postService: PostService
  ) {}

  async ngOnInit() {
    this.form = this.formBuilder.group({
      province: this.formBuilder.control(''),
      search: this.formBuilder.control(''),
      category: this.formBuilder.control(''),
      price: this.formBuilder.control(''),
      area: this.formBuilder.control(''),
      new: this.formBuilder.control(true),
      favorite: this.formBuilder.control(true),
      priceSort: this.formBuilder.control(''),
      areaSort: this.formBuilder.control(''),
    });
    this.province = this.form.get('province');
    this.search = this.form.get('search');
    this.category = this.form.get('category');
    this.price = this.form.get('price');
    this.area = this.form.get('area');
    this.new = this.form.get('new');
    this.favorite = this.form.get('favorite');
    this.priceSort = this.form.get('priceSort');
    this.areaSort = this.form.get('areaSort');

    this.options = await firstValueFrom(
      this.postService.getSearchOptions().pipe(
        takeUntilDestroyed(this.destroyRef),
        catchError(() => of([]))
      )
    );
    
    this.apartmentService.specPostParams.set({ pageSize: 10, pageIndex: 1 });
    await this.init();
  }

  async init() {
    await lastValueFrom(
      this.apartmentService.loadData().pipe(
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

  async onFilter() {
    if (this.form.dirty) {
      this.apartmentService.specPostParams.update((value) => {
        const specs: SpecPostParams = {
          ...value,
          province: this.province?.value.name,
          search: this.search?.value,
          category: this.category?.value,
          minPrice: this.price?.value.min,
          maxPrice: this.price?.value.max,
          minArea: this.area?.value.min,
          maxArea: this.area?.value.max,
          sortPrice: this.priceSort?.value,
          sortArea: this.areaSort?.value,
        };

        if (!this.new?.value && !this.favorite?.value) {
          specs.sortExtra = 'New/Favorite';
        } else if (!this.new?.value) {
          specs.sortExtra = 'New';
        } else if (!this.favorite?.value) {
          specs.sortExtra = 'Favorite';
        }

        return specs;
      });
    }
    await this.init();
  }

  async onPageChange(pageIndex: number) {
    this.apartmentService.specPostParams.update((value) => ({
      ...value,
      pageIndex: pageIndex,
    }));
    await this.init();
  }
}
