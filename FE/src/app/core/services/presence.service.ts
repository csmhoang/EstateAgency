import { Injectable, signal } from '@angular/core';
import { environment } from '@environment/environment.development';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { CookieService } from './cookie.service';
import { Result } from '@core/models/result.model';
import { Conversation } from '@features/messenger/models/conversation.model';
import { Notice } from '@features/notification/models/notification.model';

@Injectable({
  providedIn: 'root',
})
export class PresenceService {
  hubUrl = environment.apiRoot + '/hubs';
  private hubConnection!: HubConnection;
  onlineUsers = signal<string[]>([]);
  conversationThread = signal<Conversation[]>([]);
  notificationThread = signal<Notice[]>([]);

  constructor(private cookie: CookieService) {}

  createHubConnection() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.hubUrl + '/presence', {
        accessTokenFactory: () => this.cookie.get('token'),
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection.start().catch((error) => console.error(error));

    this.hubConnection.on('UserIsOnline', (id) => {
      this.onlineUsers.update((ids) => [...ids, id]);
    });

    this.hubConnection.on('UserIsOffline', (id) => {
      this.onlineUsers.update((ids) => ids.filter((x) => x !== id));
    });

    this.hubConnection.on('GetOnlineUsers', (ids: string[]) => {
      this.onlineUsers.set(ids);
    });

    this.hubConnection.on(
      'ReceiveConversationsThread',
      (conversations: Conversation[]) => {
        this.conversationThread.set(conversations);
      }
    );

    this.hubConnection.on('NewConversation', (conversation: Conversation) => {
      this.conversationThread.update((conversations) => [
        ...conversations,
        conversation,
      ]);
    });

    this.hubConnection.on(
      'ReceiveNotificationsThread',
      (notifications: Notice[]) => {
        this.notificationThread.set(notifications);
      }
    );

    this.hubConnection.on('NewNotification', (notification: Notice) => {
      this.notificationThread.update((notifications) => [
        notification,
        ...notifications,
      ]);
    });

    this.hubConnection.on('DeleteNotification', (notificationId: string) => {
      this.notificationThread.update((notifications) =>
        notifications.filter(
          (notification) => notification.id !== notificationId
        )
      );
    });
  }

  stopHubConnection() {
    if (this.hubConnection) {
      this.hubConnection.stop().catch((error) => console.error(error));
    }
  }

  async getConversation(callerId: string, otherId: string) {
    return this.hubConnection.invoke<Result<Conversation>>(
      'GetConversation',
      callerId,
      otherId
    );
  }

  async createNotification(notification: Notice) {
    return this.hubConnection.invoke('CreateNotification', notification);
  }

  async deleteNotification(notificationId: string) {
    return this.hubConnection.invoke('DeleteNotification', notificationId);
  }
}
