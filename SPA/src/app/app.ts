import { Component, inject, Signal, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Main } from "./main/main";
import { ChatCreateModal } from "./chat/chat-create-modal/chat-create-modal";
import { LoginScreen } from './login-screen/login-screen';
import { AuthService } from './_services/auth-service';
import { currentUser } from './_models/current-user';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Main, ChatCreateModal, LoginScreen],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('SPA');
  private readonly _authService = inject(AuthService);

  user = this._authService.currentUser;
}
