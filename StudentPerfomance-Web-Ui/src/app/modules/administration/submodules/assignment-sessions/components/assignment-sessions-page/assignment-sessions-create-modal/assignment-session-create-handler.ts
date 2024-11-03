import { EventEmitter } from '@angular/core';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { AssignmentSession } from '../../../models/assignment-session-interface';
import { AssignmentSessionCreatedNotification } from './assignment-session-create-notification';
import { Observable } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { IOperationHandler } from '../../../../../../../shared/models/ihandable-component-interface/Ioperation-handler-interface';

type HandlerDependencies = {
  service: UserOperationNotificationService;
  success: EventEmitter<void>;
  failure: EventEmitter<void>;
};

const createHandler =
  (dependencies: HandlerDependencies) =>
  (parameter: AssignmentSession): void => {
    const message = new AssignmentSessionCreatedNotification().buildMessage(
      parameter
    );
    dependencies.service.SetMessage = message;
    dependencies.success.emit();
  };

const createErrorHandler =
  (dependencies: HandlerDependencies) =>
  (error: HttpErrorResponse): Observable<never> => {
    dependencies.service.SetMessage = error.error;
    dependencies.failure.emit();
    return new Observable();
  };

export const AssignmentSessionCreateHandler = (
  service: UserOperationNotificationService,
  success: EventEmitter<void>,
  failure: EventEmitter<void>
): IOperationHandler<AssignmentSession> => {
  const dependencies: HandlerDependencies = {
    service: service,
    success: success,
    failure: failure,
  };
  const handle = createHandler(dependencies);
  const handleError = createErrorHandler(dependencies);
  return { handle, handleError };
};
