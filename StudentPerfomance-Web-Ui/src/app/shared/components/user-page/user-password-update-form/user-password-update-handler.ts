import { EventEmitter } from '@angular/core';
import { UserOperationNotificationService } from '../../../services/user-notifications/user-operation-notification-service.service';
import { User } from '../../../../modules/users/services/user-interface';
import { HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IOperationHandler } from '../../../models/ihandable-component-interface/Ioperation-handler-interface';

type HandlerDependencies = {
  successEmitter: EventEmitter<void>;
  failureEmitter: EventEmitter<void>;
  notificationService: UserOperationNotificationService;
};

const buildHandler =
  (dependencies: HandlerDependencies) =>
  (parameter: User): void => {
    dependencies.notificationService.SetMessage =
      'Ваш пароль был успешно изменён';
    dependencies.successEmitter.emit();
  };

const buildErrorHandler =
  (dependencies: HandlerDependencies) =>
  (error: HttpErrorResponse): Observable<never> => {
    dependencies.notificationService.SetMessage = error.error;
    dependencies.failureEmitter.emit();
    return new Observable();
  };

export const UserPasswordUpdateHandler = (
  successEmitter: EventEmitter<void>,
  failureEmitter: EventEmitter<void>,
  notificationService: UserOperationNotificationService,
): IOperationHandler<User> => {
  const dependencies: HandlerDependencies = {
    successEmitter: successEmitter,
    failureEmitter: failureEmitter,
    notificationService: notificationService,
  };
  const handle = buildHandler(dependencies);
  const handleError = buildErrorHandler(dependencies);
  return { handle, handleError };
};
