import { Component, effect, ElementRef, inject, Signal, ViewChild } from '@angular/core';
import { ChatService } from '../../_services/chat-service';
import { Chat } from '../../_models/chat';
import { Message } from '../../_models/message';
import { merge, Observable, of, scan, switchMap, tap } from 'rxjs';
import { MessageService } from '../../_services/message-service';
import { AsyncPipe, DatePipe } from '@angular/common';
import { toObservable } from '@angular/core/rxjs-interop';
import { InputArea } from '../input-area/input-area';
import { SignalrService } from '../../_services/signalr-service';

@Component({
  selector: 'app-message-screen',
  imports: [AsyncPipe, InputArea, DatePipe],
  templateUrl: './message-screen.html',
  styleUrl: './message-screen.css',
})
export class MessageScreen {
  @ViewChild('chatContainer') chatContainer!: ElementRef;

  private _chatService = inject(ChatService);
  private _messageService = inject(MessageService);
  private _signalRService = inject(SignalrService);

  currentChat: Signal<Chat | null> = this._chatService.selectedChat;
  connectionState: Signal<boolean> = this._signalRService.connectionState;
  chatId: number | null = null;
  messageList$: Observable<Message[]> = toObservable(this.currentChat).pipe(
    switchMap(chat => {
      if (chat && chat.id){
        const history$ = this._messageService.getMessages(chat!.id);
        const liveMessages$ = this._signalRService.newMessage$;

        return merge(history$, liveMessages$).pipe(
          scan((acumulator: Message[], newValue: Message[] | Message) => {
            if (Array.isArray(newValue)){
              return newValue;
            }
            return [...acumulator, newValue];
          }, []),
          tap(() => {
            setTimeout(() => {
              this.scrollDown();
            }, 10);
          })
        )
      }
      else{
        return of([])
      }
    })
  );
  
  constructor() {
    this._signalRService.startConnection();
    
    effect((onCleanUp) => {
      const chat = this.currentChat();
      const connected = this.connectionState();
      
      if (chat && chat.id && connected){
        const currentId = chat.id;
        this.chatId = chat.id;
        this._signalRService.joinChat(currentId);
        
        onCleanUp(() => {
          this._signalRService.leaveChat(currentId);
        })
      }
    })
  }

  ngOnDestroy() {
    this._signalRService.stopConnection();
  }

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