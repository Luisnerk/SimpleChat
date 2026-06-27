import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { AuthService } from '../_services/auth-service';

export const authGuard: CanActivateFn = (route, state) => {
   const _authService = inject(AuthService);

   return _authService.isLogged();
};
