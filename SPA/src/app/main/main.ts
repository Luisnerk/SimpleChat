import { Component, inject, signal, viewChild } from '@angular/core';
import { ChatList } from "../chat/chat-list/chat-list";
import { ChatService } from '../_services/chat-service';
import { MessageScreen } from '../chat/message-screen/message-screen';
import { effect } from '@angular/core';
import { AuthService } from '../_services/auth-service';
import { ChatCreateModal } from '../chat/chat-create-modal/chat-create-modal';

@Component({
  selector: 'app-main',
  imports: [ChatList, MessageScreen, ChatCreateModal],
  templateUrl: './main.html',
  styleUrl: './main.css',
})
export class Main {
  private _chatService = inject(ChatService);
  private _authService = inject(AuthService);
  private _chatCreate = viewChild(ChatCreateModal);
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

  logOut() {
    this._authService.logOut();
  }

  openNewChatModal() {
    this._chatCreate()?.open();
  }
}
