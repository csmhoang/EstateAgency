import { Injectable, signal } from '@angular/core';
import { environment } from '@environment/environment.development';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { CookieService } from './cookie.service';

@Injectable({
  providedIn: 'root',
})
export class PresenceService {
  hubUrl = environment.apiRoot + '/hubs';
  private hubConnection!: HubConnection;
  onlineUsers = signal<string[]>([]);

  constructor(private cookie: CookieService) {}

  createHubConnection() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.hubUrl + '/presence', {
        accessTokenFactory: () => this.cookie.get('token'),
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection.start().catch((error) => console.error(error));

    this.hubConnection.on('UserIsOnline', (username) => {
      this.onlineUsers.update((usernames) => [...usernames, username]);
    });

    this.hubConnection.on('UserIsOffline', (username) => {
      this.onlineUsers.update((usernames) =>
        usernames.filter((x) => x !== username)
      );
    });

    this.hubConnection.on('GetOnlineUsers', (usernames: string[]) => {
      this.onlineUsers.set(usernames);
    });
  }

  stopHubConnection() {
    if (this.hubConnection) {
      this.hubConnection.stop().catch((error) => console.error(error));
    }
  }
}
