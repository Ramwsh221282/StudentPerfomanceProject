import { UserOperationNotificationService } from '../../../services/user-notifications/user-operation-notification-service.service';
import { User } from '../../../../modules/users/services/user-interface';
import { HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IOperationHandler } from '../../../models/ihandable-component-interface/Ioperation-handler-interface';

type HandlerDependencies = {
  notificationService: UserOperationNotificationService;
};

const buildHandler =
  (dependencies: HandlerDependencies) =>
  (parameter: User): void => {
    dependencies.notificationService.SetMessage =
      'Ваш пароль был успешно изменён';
    dependencies.notificationService.success();
  };

const buildErrorHandler =
  (dependencies: HandlerDependencies) =>
  (error: HttpErrorResponse): Observable<never> => {
    dependencies.notificationService.SetMessage = error.error;
    dependencies.notificationService.failure();
    return new Observable();
  };

export const UserPasswordUpdateHandler = (
  notificationService: UserOperationNotificationService,
): IOperationHandler<User> => {
  const dependencies: HandlerDependencies = {
    notificationService: notificationService,
  };
  const handle = buildHandler(dependencies);
  const handleError = buildErrorHandler(dependencies);
  return { handle, handleError };
};
