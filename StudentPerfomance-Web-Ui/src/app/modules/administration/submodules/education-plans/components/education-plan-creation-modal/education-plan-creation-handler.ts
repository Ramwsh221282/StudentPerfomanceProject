import { HttpErrorResponse } from '@angular/common/http';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { EducationPlan } from '../../models/education-plan-interface';
import { EducationPlanCreationNotification } from './education-plan-creation-notification';
import { Observable } from 'rxjs';
import { IOperationHandler } from '../../../../../../shared/models/ihandable-component-interface/Ioperation-handler-interface';
import { EventEmitter } from '@angular/core';

type HandlerDependencies = {
  notificationService: UserOperationNotificationService;
  success: EventEmitter<void>;
  failure: EventEmitter<void>;
  refresh: EventEmitter<void>;
  visibility: EventEmitter<boolean>;
};

const createHandler =
  (dependencies: HandlerDependencies) =>
  (paramater: EducationPlan): void => {
    const message = new EducationPlanCreationNotification().buildMessage(
      paramater
    );
    dependencies.notificationService.SetMessage = message;
    dependencies.success.emit();
    dependencies.refresh.emit();
    dependencies.visibility.emit(false);
  };

const createErrorHandler =
  (dependecies: HandlerDependencies) =>
  (error: HttpErrorResponse): Observable<never> => {
    dependecies.notificationService.SetMessage = error.error;
    dependecies.failure.emit();
    dependecies.visibility.emit(false);
    return new Observable();
  };

export const EducationPlanCreationHandler = (
  notificationService: UserOperationNotificationService,
  success: EventEmitter<void>,
  failure: EventEmitter<void>,
  refresh: EventEmitter<void>,
  visibility: EventEmitter<boolean>
): IOperationHandler<EducationPlan> => {
  const dependencies: HandlerDependencies = {
    notificationService: notificationService,
    success: success,
    failure: failure,
    refresh: refresh,
    visibility: visibility,
  };
  const handle = createHandler(dependencies);
  const handleError = createErrorHandler(dependencies);
  return { handle, handleError };
};
