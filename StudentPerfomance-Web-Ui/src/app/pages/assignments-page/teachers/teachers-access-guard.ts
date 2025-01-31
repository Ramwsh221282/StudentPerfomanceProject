import { inject } from '@angular/core';
import { AuthService } from '../../../pages/user-page/services/auth.service';
import { Router } from '@angular/router';

export const CanActivateAuthTeacher = () => {
  const authService = inject(AuthService);
  if (authService.isAuthorized) {
    if (authService.userData.role != ' ' && authService.userData != null) {
      if (
        authService.userData.role == 'Преподаватель' ||
        authService.userData.role == 'Администратор'
      ) {
        return true;
      }
    }
  }

  return inject(Router).createUrlTree(['/login']);
};
