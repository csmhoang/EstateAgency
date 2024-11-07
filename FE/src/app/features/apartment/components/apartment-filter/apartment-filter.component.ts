import { CommonModule } from '@angular/common';
import { Component, DestroyRef, inject, OnInit, output } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
} from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { ApartmentService } from '@features/apartment/services/apartment.service';
import { RoomService } from '@features/apartment/services/room.service';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { SearchComponent } from '@shared/components/form/search/search.component';
import { catchError, firstValueFrom, of } from 'rxjs';

@Component({
  selector: 'app-apartment-filter',
  standalone: true,
  imports: [
    SearchComponent,
    NgbDropdownModule,
    CommonModule,
    ReactiveFormsModule,
  ],
  templateUrl: './apartment-filter.component.html',
  styleUrl: './apartment-filter.component.scss',
})
export class ApartmentFilterComponent implements OnInit {
  search = output<string>();
  destroyRef = inject(DestroyRef);

  provices$ = firstValueFrom(
    this.roomService.getProvince().pipe(
      takeUntilDestroyed(this.destroyRef),
      catchError(() => of(null))
    )
  );

  form: FormGroup = new FormGroup({});

  province?: AbstractControl | null;

  constructor(
    private formBuilder: FormBuilder,
    private apartmentService: ApartmentService,
    private roomService: RoomService
  ) {}

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      name: this.formBuilder.control(''),
      category: this.formBuilder.control(''),
      province: this.formBuilder.control(''),
      price: this.formBuilder.control(''),
      area: this.formBuilder.control(''),
    });
  }

  async onFilter() {}
}
