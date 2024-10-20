import { HttpErrorResponse } from '@angular/common/http';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { EducationDirection } from '../../models/education-direction-interface';
import { FacadeService } from '../../services/facade.service';
import { EducationDirectionEditNotificationBulder } from './education-direction-edit-notification';
import { Observable } from 'rxjs';
import { IOperationHandler } from '../../../../../../shared/models/ihandable-component-interface/Ioperation-handler-interface';
import { EventEmitter } from '@angular/core';

type HandlerDependencies = {
  oldDirection: EducationDirection;
  facadeService: FacadeService;
  notificationService: UserOperationNotificationService;
  success: EventEmitter<void>;
  failure: EventEmitter<void>;
  refresh: EventEmitter<void>;
  visibility: EventEmitter<boolean>;
};

const createHandler =
  (dependencies: HandlerDependencies) =>
  (parameter: EducationDirection): void => {
    const message = new EducationDirectionEditNotificationBulder(
      dependencies.oldDirection
    ).buildMessage(parameter);
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

export const CreateEducationDirectionEditHandler = (
  oldDirection: EducationDirection,
  facadeService: FacadeService,
  notificationService: UserOperationNotificationService,
  success: EventEmitter<void>,
  failure: EventEmitter<void>,
  refresh: EventEmitter<void>,
  visibility: EventEmitter<boolean>
): IOperationHandler<EducationDirection> => {
  const dependecies: HandlerDependencies = {
    oldDirection: oldDirection,
    facadeService: facadeService,
    notificationService: notificationService,
    success: success,
    failure: failure,
    refresh: refresh,
    visibility: visibility,
  };
  const handle = createHandler(dependecies);
  const handleError = createErrorHandler(dependecies);
  return { handle, handleError };
};
