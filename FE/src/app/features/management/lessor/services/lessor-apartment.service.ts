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
  public specParams = signal<SpecParams>({});

  constructor(private roomService: RoomService) {}

  loadData(isDisplayMiniLoading: boolean = true) {
    return this.roomService.getList(this.specParams(), isDisplayMiniLoading).pipe(
      tap({
        next: (page) => {
          if (page) {
            this.pageSignal.set(page);
          }
        },
      })
    );
  }

  delete(roomId: string) {
    return this.roomService.delete(roomId);
  }
}
