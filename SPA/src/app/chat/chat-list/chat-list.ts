import { Component, inject } from '@angular/core';
import { ChatService } from '../../_services/chat-service';
import { Observable } from 'rxjs';
import { AsyncPipe } from '@angular/common';
import { Chat } from '../../_models/chat';


@Component({
  selector: 'app-chat-list',
  imports: [AsyncPipe],
  templateUrl: './chat-list.html',
  styleUrl: './chat-list.css',
})
export class ChatList {
  chatService = inject(ChatService);
  chatList$: Observable<Chat[]> = this.chatService.getChatList();

  ngOnInit() {
  }

  selectChat(chat: Chat) {
    this.chatService.setChat(chat);
  }
}
