import { Injectable, signal } from '@angular/core';
import { PageData } from '@core/models/page-data.model';
import { SpecParams } from '@core/models/spec-params.model';
import { Room } from '@features/apartment/models/room.model';
import { RoomService } from '@features/apartment/services/room.service';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LessorApartmentService {
  pageSignal = signal<PageData<Room[]> | null>(null);
  public page = this.pageSignal.asReadonly();
  public specParams = signal<SpecParams>({
    pageSize: 5,
    pageIndex: 1,
  });

  constructor(private roomService: RoomService) {}

  loadData(isHideLoading: boolean = false) {
    return this.roomService.getList(this.specParams(), isHideLoading).pipe(
      tap({
        next: (page) => {
          if (page) {
            this.pageSignal.set(page);
          }
        },
      })
    );
  }

  delete(idRoom: string) {
    return this.roomService.delete(idRoom);
  }
}
