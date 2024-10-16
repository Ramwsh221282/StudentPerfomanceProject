import { EventEmitter } from '@angular/core';
import { UserOperationNotificationService } from '../../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { Teacher } from '../../../../../teachers/models/teacher.interface';
import { TeacherEditNotification } from './teacher-edit-notification';
import { HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IOperationHandler } from '../../../../../../../../shared/models/ihandable-component-interface/Ioperation-handler-interface';

type HandlerDependencies = {
  service: UserOperationNotificationService;
  success: EventEmitter<void>;
  failure: EventEmitter<void>;
  initial: Teacher;
};

const createHandler =
  (dependencies: HandlerDependencies) =>
  (parameter: Teacher): void => {
    const message = new TeacherEditNotification(
      dependencies.initial
    ).buildMessage(parameter);
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

export const TeacherEditHandler = (
  service: UserOperationNotificationService,
  success: EventEmitter<void>,
  failure: EventEmitter<void>,
  initial: Teacher
): IOperationHandler<Teacher> => {
  const dependencies: HandlerDependencies = {
    service: service,
    success: success,
    failure: failure,
    initial: initial,
  };
  const handle = createHandler(dependencies);
  const handleError = createErrorHandler(dependencies);
  return { handle, handleError };
};
