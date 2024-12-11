import { HttpClient, HttpContext } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Result } from '@core/models/result.model';
import { Conversation } from '../models/conversation.model';
import { SkipPreloader } from '@core/interceptors/skip.resolver';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ConversationService {
  constructor(private http: HttpClient) {}

  getAllOfCurrent(isHideLoading: boolean = false) {
    return this.http
      .get<Result<Conversation[]>>('/conversations/allOfUserCurrent', {
        context: new HttpContext().set(SkipPreloader, isHideLoading),
      })
      .pipe(map((response) => response.data));
  }

  getTwoUserId(otherId: string) {
    return this.http.get<Result<Conversation>>(
      `/conversations/otherId/${otherId}`
    );
  }
}
