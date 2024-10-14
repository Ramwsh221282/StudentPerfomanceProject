import { EventEmitter } from '@angular/core';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { Department } from '../../../models/departments.interface';
import { DepartmentDeletionNotification } from './department-deletion-notification';
import { HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IOperationHandler } from '../../../../../../../shared/models/ihandable-component-interface/Ioperation-handler-interface';

type HandlerDependencies = {
  service: UserOperationNotificationService;
  successEmitter: EventEmitter<void>;
  failureEmitter: EventEmitter<void>;
};

const createHandler =
  (dependencies: HandlerDependencies) =>
  (parameter: Department): void => {
    const message = new DepartmentDeletionNotification().buildMessage(
      parameter
    );
    dependencies.service.SetMessage = message;
    dependencies.successEmitter.emit();
  };

const createErrorHandler =
  (dependencies: HandlerDependencies) =>
  (error: HttpErrorResponse): Observable<never> => {
    dependencies.service.SetMessage = error.error;
    dependencies.failureEmitter.emit();
    return new Observable();
  };

export const DepartmentDeletionHandler = (
  service: UserOperationNotificationService,
  successEmitter: EventEmitter<void>,
  failureEmitter: EventEmitter<void>
): IOperationHandler<Department> => {
  const dependencies: HandlerDependencies = {
    service: service,
    successEmitter: successEmitter,
    failureEmitter: failureEmitter,
  };
  const handle = createHandler(dependencies);
  const handleError = createErrorHandler(dependencies);
  return { handle, handleError };
};
