import { UserOperationNotificationService } from '../../../shared/services/user-notifications/user-operation-notification-service.service';
import { User } from '../services/user-interface';
import { IOperationHandler } from '../../../shared/models/ihandable-component-interface/Ioperation-handler-interface';
import { HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';

type HandlerDependencies = {
  notificationnService: UserOperationNotificationService;
};

const createHandler =
  (dependencies: HandlerDependencies) =>
  (parameter: User): void => {
    dependencies.notificationnService.SetMessage = 'Ваша почта была изменена';
    dependencies.notificationnService.success();
  };

const createErrorHandler =
  (dependencies: HandlerDependencies) =>
  (error: HttpErrorResponse): Observable<never> => {
    dependencies.notificationnService.SetMessage = error.error;
    dependencies.notificationnService.failure();
    return new Observable();
  };

export const UserEmailUpdateHandler = (
  notificationService: UserOperationNotificationService,
): IOperationHandler<User> => {
  const dependencies: HandlerDependencies = {
    notificationnService: notificationService,
  };
  const handle = createHandler(dependencies);
  const handleError = createErrorHandler(dependencies);
  return { handle, handleError };
};
