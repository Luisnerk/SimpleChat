import { Component, effect, Signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { inject } from '@angular/core';
import { MessageService } from '../../_services/message-service';
import { ChatService } from '../../_services/chat-service';
import { Chat } from '../../_models/chat';

@Component({
  selector: 'app-input-area',
  imports: [ReactiveFormsModule],
  templateUrl: './input-area.html',
  styleUrl: './input-area.css',
})
export class InputArea {
  private _fb = inject(FormBuilder);
  private _messageService = inject(MessageService);
  private _chatService = inject(ChatService);

  messageForm: FormGroup = new FormGroup({});
  private _currentChat: Signal<Chat | null> = this._chatService.selectedChat;
  
  constructor() {
    effect(() => {
      const chat = this._currentChat();
      this.messageForm.setValue({content: ""});
    });
  }
  
  ngOnInit() {
    this.messageForm = this._fb.group({
      content: ["", Validators.required]
    })
    
  }

  submitMessageForm(){
    let sub = this._messageService.sendMessage(this.messageForm.value.content).subscribe({
      complete: () => this.messageForm.setValue({content: ""})
    });
  }
}
