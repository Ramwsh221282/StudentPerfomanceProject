import { HttpErrorResponse } from '@angular/common/http';
import { EducationDirection } from '../../models/education-direction-interface';
import { EducationCreatedNotificationBuilder } from './education-directions-create-notification';
import { Observable } from 'rxjs';
import { IOperationHandler } from '../../../../../../shared/models/ihandable-component-interface/Ioperation-handler-interface';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { EventEmitter } from '@angular/core';

type HandlerDependencies = {
  notificationService: UserOperationNotificationService;
  refresh: EventEmitter<void>;
};

const createHandler =
  (dependencies: HandlerDependencies) =>
  (parameter: EducationDirection): void => {
    const message = new EducationCreatedNotificationBuilder().buildMessage(
      parameter,
    );
    dependencies.refresh.emit();
    dependencies.notificationService.SetMessage = message;
    dependencies.notificationService.success();
  };

const createErrorHandler =
  (dependencies: HandlerDependencies) =>
  (error: HttpErrorResponse): Observable<never> => {
    dependencies.notificationService.SetMessage = error.error;
    dependencies.notificationService.failure();
    return new Observable();
  };

export const EducationDirectionCreationHandler = (
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