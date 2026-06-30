import { HttpClient, HttpParams } from '@angular/common/http';
import { signal, Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { ChatCreate } from '../_models/chat-create';
import { Chat } from '../_models/chat';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ChatService {
  constructor(private http: HttpClient) {}
  
  baseUrl: string = environment.apiUrl + "Chat/";

  private selectedChatSignal = signal<Chat | null> (null);
  readonly selectedChat = this.selectedChatSignal.asReadonly();

  setChat(chat: Chat) {
    this.selectedChatSignal.set(chat);
  }

  createChat(model: ChatCreate) {
    return this.http.post(this.baseUrl + 'create', model, {observe: "response"});
  }

  getChatList(): Observable<Chat[]> {
    return this.http.get<Chat[]>(this.baseUrl + "getChatList")
  }
}
