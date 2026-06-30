import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { inject } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { Observable } from 'rxjs';
import { Message } from '../_models/message';

@Injectable({
  providedIn: 'root',
})
export class MessageService {
  private http = inject(HttpClient);
  private baseUrl = environment.apiUrl + "Message/";

  public sendMessage(content: string, chatId: number){

    return this.http.post(this.baseUrl + "send", {content: content, chatId: chatId});
  }

  public getMessages(chatId: number): Observable<Message[]>{
    var params = new HttpParams().set("chatId", chatId);
    return this.http.get<Message[]>(this.baseUrl + "getMessages", {params: params});
  }
}
