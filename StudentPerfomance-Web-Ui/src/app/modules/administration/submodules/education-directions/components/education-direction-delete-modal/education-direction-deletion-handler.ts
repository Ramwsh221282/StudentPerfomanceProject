import { HttpErrorResponse } from '@angular/common/http';
import { EducationDirection } from '../../models/education-direction-interface';
import { Observable } from 'rxjs';
import { IOperationHandler } from '../../../../../../shared/models/ihandable-component-interface/Ioperation-handler-interface';
import { EventEmitter } from '@angular/core';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { EducationDirectionDeleteNotification } from './education-direction-delete-notification';

type HandlerDependencies = {
  notificationService: UserOperationNotificationService;
  refresh: EventEmitter<void>;
};

const createHandler =
  (dependencies: HandlerDependencies) =>
  (parameter: EducationDirection): void => {
    dependencies.notificationService.SetMessage =
      new EducationDirectionDeleteNotification().buildMessage(parameter);
    dependencies.notificationService.success();
    dependencies.refresh.emit();
  };

const createErrorHandler =
  (dependencies: HandlerDependencies) =>
  (error: HttpErrorResponse): Observable<never> => {
    dependencies.notificationService.SetMessage = error.error;
    dependencies.notificationService.failure();
    return new Observable();
  };

export const EducationDirectionDeletionHandler = (
  notificationService: UserOperationNotificationService,
  refresh: EventEmitter<void>,
): IOperationHandler<EducationDirection> => {
  const dependencies: HandlerDependencies = {
    notificationService: notificationService,
    refresh: refresh,
  };
  const handle = createHandler(dependencies);
  const handleError = createErrorHandler(dependencies);
  return { handle, handleError };
};
