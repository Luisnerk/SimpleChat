import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../_services/auth-service';

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  const _authService = inject(AuthService);
  const token = _authService.getCurrentUser()?.token;

  if (token){
    const cloneRequest = req.clone({
      setHeaders: {
        Authorization: `Bearer ` + token
      }
    });
    return next(cloneRequest);
  }
  return next(req);
};
