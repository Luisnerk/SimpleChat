import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { ChatCreate } from '../_models/chat-create';

@Injectable({
  providedIn: 'root',
})
export class ChatService {
  constructor(private http: HttpClient) {}

  baseUrl: string = environment.apiUrl + "Chat/";

  createChat(model: ChatCreate) {
    return this.http.post(this.baseUrl + 'create', model, {observe: "response"});
  }
}
