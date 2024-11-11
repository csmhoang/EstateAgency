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
import { User } from '@core/models/user.model';
import { LessorListComponent } from '@features/lessor/components/lessor-list/lessor-list.component';
import { LessorService } from '@features/lessor/services/lessor.service';
import { NgbTypeahead, NgbTypeaheadModule } from '@ng-bootstrap/ng-bootstrap';
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
  selector: 'app-lessor',
  standalone: true,
  imports: [
    HeaderComponent,
    FooterComponent,
    LessorListComponent,
    RouterLink,
    CommonModule,
    ReactiveFormsModule,
    NgbTypeaheadModule,
    FormsModule,
    PaginationComponent,
    MiniLoadComponent,
  ],
  templateUrl: './lessor.component.html',
  styleUrl: './lessor.component.scss',
})
export class LessorComponent implements OnInit {
  destroyRef = inject(DestroyRef);
  lessors = signal<User[]>([]);
  paginationParams = signal<PaginationParams>({
    pageSize: 0,
    count: 0,
    pageIndex: 1,
  });

  options: string[] = [];

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
  search?: AbstractControl | null;

  constructor(
    private formBuilder: FormBuilder,
    private lessorService: LessorService
  ) {}

  async ngOnInit() {
    this.form = this.formBuilder.group({
      search: this.formBuilder.control(''),
    });

    this.search = this.form.get('search');

    this.options = await firstValueFrom(
      this.lessorService.getSearchOptions().pipe(
        takeUntilDestroyed(this.destroyRef),
        catchError(() => of([]))
      )
    );

    this.lessorService.specUserParams.set({
      pageSize: 6,
      pageIndex: 1,
      roles: 'landlord',
    });
    await this.init();
  }

  onFilter() {
    console.log(this.form.value);
  }

  async init() {
    await lastValueFrom(
      this.lessorService.loadData().pipe(
        takeUntilDestroyed(this.destroyRef),
        catchError(() => of(null))
      )
    );

    const page = this.lessorService.page();
    if (page) {
      this.paginationParams.set(page);
      this.lessors.set(page.data);
    }
  }

  async onPageChange(pageIndex: number) {
    this.lessorService.specUserParams.update((value) => ({
      ...value,
      pageIndex: pageIndex,
    }));
    await this.init();
  }
}
