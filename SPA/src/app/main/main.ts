import { Component } from '@angular/core';
import { Chat } from '../_services/chat';

@Component({
  selector: 'app-main',
  imports: [],
  templateUrl: './main.html',
  styleUrl: './main.css',
})
export class Main {
  constructor (private chatService: Chat) {}

  temporal() {
    console.log("hola");
    this.chatService.createChat().subscribe();
  }
}
