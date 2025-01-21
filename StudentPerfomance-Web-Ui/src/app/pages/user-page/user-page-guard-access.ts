import { inject } from '@angular/core';
import { AuthService } from './services/auth.service';
import { Router } from '@angular/router';

export const CanActivateUserPage = () => {
  const authService = inject(AuthService);
  if (authService.isAuthorized) {
    return true;
  }

  return inject(Router).createUrlTree(['/login']);
};
