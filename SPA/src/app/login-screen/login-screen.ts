import { Component, effect } from '@angular/core';
import { inject } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../_services/auth-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-screen',
  imports: [ReactiveFormsModule],
  templateUrl: './login-screen.html',
  styleUrl: './login-screen.css',
})
export class LoginScreen {
  private _fb = inject(FormBuilder);
  private _authService = inject(AuthService);
  private _router = inject(Router);
  private currentUser = this._authService.currentUser;
  loginForm: FormGroup = new FormGroup({});

  constructor() {
    effect(() => {
      if (this.currentUser()){
        this._router.navigate(['main'])
      }
    })
  }

  createForm() {
    this.loginForm = this._fb.group({
      username: ["", Validators.required],
      password: ["", Validators.required]
    })
  }

  login() {
    if (this.loginForm.valid) {
      this._authService.login(this.loginForm.value).subscribe({
        error: err => console.log("Ocurrió un error al iniciar sesión")
      });
    }
    else {
      console.log("formulario inválido")
    }
  }
}
