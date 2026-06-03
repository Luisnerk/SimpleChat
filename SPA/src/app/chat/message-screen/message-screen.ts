import { Component, ElementRef, inject, Signal, ViewChild } from '@angular/core';
import { ChatService } from '../../_services/chat-service';
import { Chat } from '../../_models/chat';
import { Message } from '../../_models/message';
import { Observable, of, switchMap, tap } from 'rxjs';
import { MessageService } from '../../_services/message-service';
import { AsyncPipe } from '@angular/common';
import { toObservable } from '@angular/core/rxjs-interop';
import { InputArea } from '../input-area/input-area';

@Component({
  selector: 'app-message-screen',
  imports: [AsyncPipe, InputArea],
  templateUrl: './message-screen.html',
  styleUrl: './message-screen.css',
})
export class MessageScreen {
  private _chatService = inject(ChatService);
  private _messageService = inject(MessageService);
  @ViewChild('chatContainer') chatContainer!: ElementRef;

  currentChat: Signal<Chat | null> = this._chatService.selectedChat;
  messageList$: Observable<Message[]> = toObservable(this.currentChat).pipe(
    switchMap(chat => {
      if (chat && chat.id){
        return this._messageService.getMessages(chat!.id);
      }
      else{
        return of([])
      }
    }),
    tap(() => {
      setTimeout(() => {
        this.scrollDown();
      }, 10);
    })
  );

  scrollDown() {
    try {
      const element = this.chatContainer.nativeElement;
      element.scrollTop = element.scrollHeight;
    } catch (err) {
      console.log("Scrolling error");
      console.log(err)
    }
    
  }
  

}
