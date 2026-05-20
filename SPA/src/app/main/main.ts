import { Component, inject } from '@angular/core';
import { ChatList } from "../chat/chat-list/chat-list";
import { ChatService } from '../_services/chat-service';

@Component({
  selector: 'app-main',
  imports: [ChatList],
  templateUrl: './main.html',
  styleUrl: './main.css',
})
export class Main {
  chatService = inject(ChatService);

  currentChat = this.chatService.selectedChat;
}
