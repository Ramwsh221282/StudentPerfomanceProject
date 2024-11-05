import { inject } from '@angular/core';
import { AuthService } from '../../users/services/auth.service';
import { Router } from '@angular/router';

export const CanActivateAuthTeacher = () => {
  const authService = inject(AuthService);
  if (authService.isAuthorized) {
    if (authService.userData.role != ' ' && authService.userData != null) {
      if (authService.userData.role == 'Преподаватель') {
        return true;
      }
    }
  }

  return inject(Router).createUrlTree(['/login']);
};
