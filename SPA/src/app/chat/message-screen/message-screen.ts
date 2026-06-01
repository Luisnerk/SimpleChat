import { Component, inject, Signal } from '@angular/core';
import { ChatService } from '../../_services/chat-service';
import { Chat } from '../../_models/chat';
import { Message } from '../../_models/message';
import { Observable, of, switchMap } from 'rxjs';
import { MessageService } from '../../_services/message-service';
import { AsyncPipe } from '@angular/common';
import { toObservable } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-message-screen',
  imports: [AsyncPipe],
  templateUrl: './message-screen.html',
  styleUrl: './message-screen.css',
})
export class MessageScreen {
  private _chatService = inject(ChatService);
  private _messageService = inject(MessageService);

  currentChat: Signal<Chat | null> = this._chatService.selectedChat;
  messageList$: Observable<Message[]> = toObservable(this.currentChat).pipe(
    switchMap(chat => {
      if (chat && chat.id){
        return this._messageService.getMessages(chat!.id);
      }
      else{
        return of([])
      }
    })
  );
  

}
