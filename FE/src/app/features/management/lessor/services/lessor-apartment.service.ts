import { DestroyRef, inject, Injectable, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { PageData } from '@core/models/page-data.model';
import { RoomParams } from '@features/apartment/models/room-params.model';
import { Room } from '@features/apartment/models/room.model';
import { ApartmentService } from '@features/apartment/services/apartment.service';
import { catchError, EMPTY, lastValueFrom, map, of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LessorApartmentService {
  pageSignal = signal<PageData<Room[]> | null>(null);
  public page = this.pageSignal.asReadonly();
  public specParams = signal<RoomParams>({
    pageSize: 5,
    pageIndex: 1,
  });

  destroyRef = inject(DestroyRef);
  apartmentService = inject(ApartmentService);

  async loadData(isHideLoading: boolean = false) {
    await lastValueFrom(
      this.apartmentService.getList(this.specParams(), isHideLoading).pipe(
        takeUntilDestroyed(this.destroyRef),
        map((page) => {
          if (page) {
            this.pageSignal.set(page);
          }
          return page;
        }),
        catchError(() => of(null))
      )
    );
  }
}
