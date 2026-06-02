import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { inject } from '@angular/core';
import { MessageService } from '../../_services/message-service';

@Component({
  selector: 'app-input-area',
  imports: [ReactiveFormsModule],
  templateUrl: './input-area.html',
  styleUrl: './input-area.css',
})
export class InputArea {
  private _fb = inject(FormBuilder);
  private _messageService = inject(MessageService);

  messageForm: FormGroup = new FormGroup({});

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
