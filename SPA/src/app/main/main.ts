import { Component, inject, signal } from '@angular/core';
import { ChatList } from "../chat/chat-list/chat-list";
import { ChatService } from '../_services/chat-service';
import { MessageScreen } from '../chat/message-screen/message-screen';
import { effect } from '@angular/core';

@Component({
  selector: 'app-main',
  imports: [ChatList, MessageScreen],
  templateUrl: './main.html',
  styleUrl: './main.css',
})
export class Main {
  private _chatService = inject(ChatService);
  chatTitle = signal<string>("Selecciona un chat");

  currentChat = this._chatService.selectedChat;

  constructor() {
    effect(() => {
      if (this.currentChat() && this.currentChat()?.name){
        this.chatTitle.set(this.currentChat()!.name)
      }
      else {
        this.chatTitle.set("Selecciona un chat");
      }
    })
  }
}
