import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { ChatCreate } from '../_models/chat-create';

@Injectable({
  providedIn: 'root',
})
export class Chat {
  constructor(private http: HttpClient) {}

  baseUrl: string = environment.apiUrl + "Chat/";

  createChat() {
    const model: ChatCreate = {
      name: "chat angular",
      userIds: [1, 2]
    }
    console.log(model);
    return this.http.post(this.baseUrl + 'create', model)
  }
}
