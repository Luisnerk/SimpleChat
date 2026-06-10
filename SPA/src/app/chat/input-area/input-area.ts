import { Component, effect, input, Input, Signal } from '@angular/core';
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
  chatId = input<number | null>(null);

  messageForm: FormGroup = new FormGroup({});
  
  constructor() {
    this.messageForm = this._fb.group({
      content: ["", Validators.required]
    })

    effect(() => {
      const currentId = this.chatId();
      this.messageForm.reset();
    }) 
  }  

  submitMessageForm(){
    const chatId = this.chatId();
    if (chatId !== null){
      let sub = this._messageService.sendMessage(this.messageForm.value.content, chatId).subscribe({
        next: () => this.messageForm.reset()
      });
    }
  }
}
