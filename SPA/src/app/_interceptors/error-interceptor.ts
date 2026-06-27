import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError } from 'rxjs';
import { AuthService } from '../_services/auth-service';
import { Router } from '@angular/router';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const _authService = inject(AuthService);
  const _router = inject(Router);
  return next(req).pipe(
    catchError((error: HttpErrorResponse) =>{
      switch(error.status){
        case(401):
          _authService.logOut();
          _router.navigate(["login"]);
          break
        default:
          console.log("Error al buscar la página")
      }
      throw error;
    })
  );
};
