import { inject } from '@angular/core';
import { AuthService } from './auth.service';
import { Router } from '@angular/router';

export const canActivateAuthAdmin = () => {
  const authService = inject(AuthService);
  if (authService.isAuthorized) {
    if (authService.userData.role != ' ' && authService.userData != null) {
      if (authService.userData.role == 'Администратор') {
        return true;
      }
    }
  }

  return inject(Router).createUrlTree(['/login']);
};
