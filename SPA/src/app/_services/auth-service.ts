import { inject, Injectable, Signal, signal } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Login } from '../_models/login';
import { tap } from 'rxjs';
import { currentUser } from '../_models/current-user';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private _http = inject(HttpClient);
  private _router = inject(Router);
  
  private _currentUserSignal = signal<currentUser | null>(this.getCurrentUser());
  readonly currentUser = this._currentUserSignal.asReadonly();
  private baseUrl: string = environment.apiUrl + "Account/";

  login(credentials: Login) {
    return this._http.post<currentUser>(this.baseUrl + "login", credentials)
                .pipe(tap(response => {
                  localStorage.setItem("id", response.id)
                  localStorage.setItem("username", response.username);
                  localStorage.setItem("name", response.name);
                  localStorage.setItem("token", response.token);
                  this._currentUserSignal.set(response);
                }));
  }

  getCurrentUser(): currentUser | null {
    const id = localStorage.getItem("id")
    const username = localStorage.getItem("username");
    const name = localStorage.getItem("name");
    const token = localStorage.getItem("token");

    if (!username || !name || !token || !id){
      return null;
    }

    return {username, name, token, id};
  }

  logOut() {
    localStorage.removeItem("username");
    localStorage.removeItem("name");
    localStorage.removeItem("token");
    // this._currentUserSignal.set(null);
    this._router.navigate(["login"]);
  }

  isLogged() {
    return !!this.getCurrentUser();
  }
}
