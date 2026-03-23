import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Main } from "./main/main";
import { ChatCreateModal } from "./chat-create-modal/chat-create-modal";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Main, ChatCreateModal],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('SPA');
}
