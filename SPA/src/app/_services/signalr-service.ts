import { Injectable, signal, Signal } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Subject } from 'rxjs';
import { Message } from '../_models/message';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class SignalrService {
  private hubConnection: signalR.HubConnection | undefined;
  private connected = signal<boolean>(false);
  readonly connectionState = this.connected.asReadonly();
  public newMessage$ = new Subject<Message>();
  private baseUrl = environment.apiUrl + "hubs/chat";

  constructor() {}

  public startConnection() {
    this.hubConnection = new signalR.HubConnectionBuilder()
                                                .withUrl(this.baseUrl)
                                                .withAutomaticReconnect()
                                                .build();
                                                this.hubConnection
                                                .start()
                                                .then(() => {
                                                  this.connected.set(true);
                                                  console.log("Started signalR connection");
                                                })
                                                .catch(
                                                  err => console.log("Error al inicial la conexión: " + err)
                                                )
      this.hubConnection.on("NewMessage", (message: Message) => {
        this.newMessage$.next(message);
      });
    }
                
  public joinChat(chatId: number){
    if (this.hubConnection){
      try{
        this.hubConnection.invoke('JoinChat', chatId.toString());
        console.log("Started connection with chat: " + chatId);
      } catch (err) {
        console.log("Ocurrió un error al unirse al chat");
      }
    }
  }

  public leaveChat(chatId: number) {
    if (this.hubConnection){
      this.hubConnection.invoke('LeaveChat', chatId.toString())
      .then(() => console.log("Finished connection with chat: " + chatId))
      .catch((err) => console.log("Error al salir: " + err));
    }
  }        

  public stopConnection() {
    if (this.hubConnection) {
      this.hubConnection.stop();
      this.connected.set(false);
    }
  }
}
