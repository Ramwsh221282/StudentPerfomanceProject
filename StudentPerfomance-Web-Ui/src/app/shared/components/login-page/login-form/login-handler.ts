import { HttpErrorResponse } from '@angular/common/http';
import { User } from '../../../../modules/users/services/user-interface';
import { UserOperationNotificationService } from '../../../services/user-notifications/user-operation-notification-service.service';
import { Observable } from 'rxjs';
import { IOperationHandler } from '../../../models/ihandable-component-interface/Ioperation-handler-interface';

type HandlerDependencies = {
  service: UserOperationNotificationService;
};

const createHandler =
  (dependencies: HandlerDependencies) =>
  (parameter: User): void => {
    dependencies.service.SetMessage = `Успешная авторизация ${parameter.name}!`;
    dependencies.service.success();
  };

const createErrorHandler =
  (dependencies: HandlerDependencies) =>
  (error: HttpErrorResponse): Observable<never> => {
    dependencies.service.SetMessage = 'Ошибка в процессе авторизации';
    dependencies.service.failure();
    return new Observable();
  };

export const LoginHandler = (
  service: UserOperationNotificationService,
): IOperationHandler<User> => {
  const dependencies: HandlerDependencies = {
    service: service,
  };
  const handle = createHandler(dependencies);
  const handleError = createErrorHandler(dependencies);
  return { handle, handleError };
};
