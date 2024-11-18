import { Injectable, signal } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Feedback } from '../models/feedback.model';
import { environment } from '@environment/environment.development';
import { CookieService } from '@core/services/cookie.service';
import { ToastService } from '@shared/services/toast/toast.service';

@Injectable({
  providedIn: 'root',
})
export class FeedbackService {
  hubUrl = environment.apiRoot + '/hubs';
  private hubConnection!: HubConnection;
  feedbackThread = signal<Feedback[]>([]);

  constructor(private cookie: CookieService) {}

  createHubConnection(postId: string) {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.hubUrl + '/feedback?postId=' + postId, {
        accessTokenFactory: () => this.cookie.get('token'),
      })
      .withAutomaticReconnect()
      .build();
    this.hubConnection.start().catch((error) => console.error(error));

    this.hubConnection.on('ReceiveFeedbacksThread', (feedback: Feedback[]) => {
      this.feedbackThread.set(feedback);
    });

    this.hubConnection.on('NewFeedback', (feedback: Feedback) => {
      this.feedbackThread.update((feednacks) => {
        const index = feednacks.findIndex((f) => f.id === feedback.replyId);
        if (index != -1) {
          feednacks[index].replies?.push(feedback);
          return feednacks;
        }
        return [...feednacks, feedback];
      });
    });
  }

  stopHubConnection() {
    if (this.hubConnection) {
      this.hubConnection.stop().catch((error) => console.error(error));
    }
  }

  async sendFeedback(feedback: Feedback) {
    return this.hubConnection.invoke('SendFeedback', feedback);
  }
}
