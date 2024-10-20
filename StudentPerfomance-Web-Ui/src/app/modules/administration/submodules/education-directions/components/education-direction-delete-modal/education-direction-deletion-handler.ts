import { HttpErrorResponse } from '@angular/common/http';
import { EducationDirection } from '../../models/education-direction-interface';
import { FacadeService } from '../../services/facade.service';
import { Observable } from 'rxjs';
import { IOperationHandler } from '../../../../../../shared/models/ihandable-component-interface/Ioperation-handler-interface';
import { EventEmitter } from '@angular/core';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { EducationDirectionDeleteNotification } from './education-direction-delete-notification';

type HandlerDependencies = {
  facadeService: FacadeService;
  notificationService: UserOperationNotificationService;
  success: EventEmitter<void>;
  failure: EventEmitter<void>;
  visibility: EventEmitter<boolean>;
  refresh: EventEmitter<void>;
};

const createHandler =
  (dependencies: HandlerDependencies) =>
  (parameter: EducationDirection): void => {
    const message = new EducationDirectionDeleteNotification().buildMessage(
      parameter
    );
    dependencies.notificationService.SetMessage = message;
    dependencies.success.emit();
    dependencies.refresh.emit();
    dependencies.visibility.emit(false);
  };

const createErrorHandler =
  (dependencies: HandlerDependencies) =>
  (error: HttpErrorResponse): Observable<never> => {
    dependencies.notificationService.SetMessage = error.error;
    dependencies.failure.emit();
    dependencies.visibility.emit(false);
    return new Observable();
  };

export const EducationDirectionDeletionHandler = (
  facadeService: FacadeService,
  notificationService: UserOperationNotificationService,
  success: EventEmitter<void>,
  failure: EventEmitter<void>,
  visibility: EventEmitter<boolean>,
  refresh: EventEmitter<void>
): IOperationHandler<EducationDirection> => {
  const dependecies: HandlerDependencies = {
    facadeService: facadeService,
    notificationService: notificationService,
    success: success,
    failure: failure,
    visibility: visibility,
    refresh: refresh,
  };
  const handle = createHandler(dependecies);
  const handleError = createErrorHandler(dependecies);
  return { handle, handleError };
};
