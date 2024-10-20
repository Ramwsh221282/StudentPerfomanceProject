import { EventEmitter } from '@angular/core';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { UserRecord } from '../../services/user-table-element-interface';
import { UserCreationNotification } from './users-create-notification';
import { HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IOperationHandler } from '../../../../../../shared/models/ihandable-component-interface/Ioperation-handler-interface';

type HandlerDependencies = {
  service: UserOperationNotificationService;
  success: EventEmitter<void>;
  failure: EventEmitter<void>;
  refresh: EventEmitter<void>;
  visibility: EventEmitter<boolean>;
};

const createHandler =
  (dependencies: HandlerDependencies) =>
  (parameter: UserRecord): void => {
    const message = new UserCreationNotification().buildMessage(parameter);
    dependencies.service.SetMessage = message;
    dependencies.refresh.emit();
    dependencies.success.emit();
    dependencies.visibility.emit(false);
  };

const createErrorHandler =
  (dependencies: HandlerDependencies) =>
  (error: HttpErrorResponse): Observable<never> => {
    dependencies.service.SetMessage = error.error;
    dependencies.failure.emit();
    dependencies.visibility.emit(false);
    return new Observable();
  };

export const UserCreationHandler = (
  service: UserOperationNotificationService,
  success: EventEmitter<void>,
  failure: EventEmitter<void>,
  refresh: EventEmitter<void>,
  visibility: EventEmitter<boolean>
): IOperationHandler<UserRecord> => {
  const dependencies: HandlerDependencies = {
    service: service,
    success: success,
    failure: failure,
    refresh: refresh,
    visibility: visibility,
  };
  const handle = createHandler(dependencies);
  const handleError = createErrorHandler(dependencies);
  return { handle, handleError };
};