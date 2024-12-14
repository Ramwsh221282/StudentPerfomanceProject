import { HttpErrorResponse } from '@angular/common/http';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { StudentGroup } from '../../../services/studentsGroup.interface';
import { GroupNameChangeNotification } from './group-change-notification';
import { Observable } from 'rxjs';
import { IOperationHandler } from '../../../../../../../shared/models/ihandable-component-interface/Ioperation-handler-interface';
import { EventEmitter } from '@angular/core';

type HandlerDependencies = {
  notificationService: UserOperationNotificationService;
  success: EventEmitter<void>;
  failure: EventEmitter<void>;
  refresh: EventEmitter<void>;
  initial: StudentGroup;
};

const createHandler =
  (dependencies: HandlerDependencies) =>
  (parameter: StudentGroup): void => {
    const message = new GroupNameChangeNotification(
      dependencies.initial,
    ).buildMessage(parameter);
    dependencies.notificationService.SetMessage = message;
    dependencies.refresh.emit();
    dependencies.success.emit();
  };

const createErrorHandler =
  (dependencies: HandlerDependencies) =>
  (error: HttpErrorResponse): Observable<never> => {
    dependencies.notificationService.SetMessage = error.error;
    dependencies.failure.emit();
    return new Observable();
  };

export const GroupNameChangeHandler = (
  notificationService: UserOperationNotificationService,
  success: EventEmitter<void>,
  failure: EventEmitter<void>,
  refresh: EventEmitter<void>,
  initial: StudentGroup,
): IOperationHandler<StudentGroup> => {
  const dependencies: HandlerDependencies = {
    notificationService: notificationService,
    success: success,
    failure: failure,
    refresh: refresh,
    initial: initial,
  };
  const handle = createHandler(dependencies);
  const handleError = createErrorHandler(dependencies);
  return { handle, handleError };
};
