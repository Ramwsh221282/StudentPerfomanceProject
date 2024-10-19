import { HttpErrorResponse } from '@angular/common/http';
import { User } from '../../../../modules/users/services/user-interface';
import { IFailureNotificatable } from '../../../models/interfaces/ifailure-notificatable';
import { ISuccessNotificatable } from '../../../models/interfaces/isuccess-notificatable';
import { UserOperationNotificationService } from '../../../services/user-notifications/user-operation-notification-service.service';
import { Observable } from 'rxjs';
import { IOperationHandler } from '../../../models/ihandable-component-interface/Ioperation-handler-interface';

type HandlerDependencies = {
  service: UserOperationNotificationService;
  success: ISuccessNotificatable;
  failure: IFailureNotificatable;
};

const createHandler =
  (dependencies: HandlerDependencies) =>
  (parameter: User): void => {
    dependencies.service.SetMessage = 'Успешная авторизация';
    dependencies.success.notifySuccess();
  };

const createErrorHandler =
  (dependencies: HandlerDependencies) =>
  (error: HttpErrorResponse): Observable<never> => {
    dependencies.service.SetMessage = 'Ошибка в процессе авторизации';
    dependencies.failure.notifyFailure();
    return new Observable();
  };

export const LoginHandler = (
  service: UserOperationNotificationService,
  success: ISuccessNotificatable,
  failure: IFailureNotificatable
): IOperationHandler<User> => {
  const dependencies: HandlerDependencies = {
    service: service,
    success: success,
    failure: failure,
  };
  const handle = createHandler(dependencies);
  const handleError = createErrorHandler(dependencies);
  return { handle, handleError };
};
