import { EventEmitter } from '@angular/core';
import { UserOperationNotificationService } from '../../../services/user-notifications/user-operation-notification-service.service';
import { User } from '../../../../modules/users/services/user-interface';
import { IOperationHandler } from '../../../models/ihandable-component-interface/Ioperation-handler-interface';
import { HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';

type HandlerDependencies = {
  successEmitter: EventEmitter<void>;
  failureEmitter: EventEmitter<void>;
  notificationnService: UserOperationNotificationService;
};

const createHandler =
  (dependencies: HandlerDependencies) =>
  (parameter: User): void => {
    dependencies.notificationnService.SetMessage = 'Ваша почта была изменена';
    dependencies.successEmitter.emit();
  };

const createErrorHandler =
  (dependencies: HandlerDependencies) =>
  (error: HttpErrorResponse): Observable<never> => {
    dependencies.notificationnService.SetMessage = error.error;
    dependencies.failureEmitter.emit();
    return new Observable();
  };

export const UserEmailUpdateHandler = (
  successEmitter: EventEmitter<void>,
  failureEmitter: EventEmitter<void>,
  notificationService: UserOperationNotificationService,
): IOperationHandler<User> => {
  const dependencies: HandlerDependencies = {
    successEmitter: successEmitter,
    failureEmitter: failureEmitter,
    notificationnService: notificationService,
  };
  const handle = createHandler(dependencies);
  const handleError = createErrorHandler(dependencies);
  return { handle, handleError };
};
