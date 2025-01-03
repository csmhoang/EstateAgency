import { Injectable, signal } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Feedback } from '../models/feedback.model';
import { environment } from '@environment/environment.development';
import { CookieService } from '@core/services/cookie.service';

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
      debugger
      this.feedbackThread.set(feedback);
    });

    this.hubConnection.on('NewFeedback', (feedback: Feedback) => {
      this.feedbackThread.update((feedbacks) => {
        const index = feedbacks.findIndex((f) => f.id === feedback.replyId);
        if (index != -1) {
          feedbacks[index].replies?.push(feedback);
          return feedbacks;
        }
        return [...feedbacks, feedback];
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
