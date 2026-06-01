import { Component, inject } from '@angular/core';
import { ChatList } from "../chat/chat-list/chat-list";
import { ChatService } from '../_services/chat-service';
import { MessageScreen } from '../chat/message-screen/message-screen';

@Component({
  selector: 'app-main',
  imports: [ChatList, MessageScreen],
  templateUrl: './main.html',
  styleUrl: './main.css',
})
export class Main {
  private _chatService = inject(ChatService);

  currentChat = this._chatService.selectedChat;
}
