import { Injectable, signal } from '@angular/core';
import { environment } from '@environment/environment.development';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Message } from '../models/message.model';
import { CookieService } from '@core/services/cookie.service';

@Injectable({
  providedIn: 'root',
})
export class MessageService {
  hubUrl = environment.apiRoot + '/hubs';
  private hubConnection!: HubConnection;
  messageThread = signal<Message[]>([]);

  constructor(private cookie: CookieService) {}

  createHubConnection(conversationId: string) {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.hubUrl + '/message?conversationId=' + conversationId, {
        accessTokenFactory: () => this.cookie.get('token'),
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection.start().catch((error) => console.log(error));

    this.hubConnection.on('ReceiveMessagesThread', (messages: Message[]) => {
      this.messageThread.set(messages);
    });

    this.hubConnection.on('NewMessage', (message: Message) => {
      this.messageThread.update((messages) => [...messages, message]);
    });
  }
  stopHubConnection() {
    if (this.hubConnection) {
      this.hubConnection.stop();
    }
  }

  async sendMessage(message: Message) {
    return this.hubConnection.invoke('SendMessage', message);
  }
}
