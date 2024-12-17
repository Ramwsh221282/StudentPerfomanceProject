import { HttpErrorResponse } from '@angular/common/http';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { EducationPlan } from '../../models/education-plan-interface';
import { EducationPlanCreationNotification } from './education-plan-creation-notification';
import { Observable } from 'rxjs';
import { IOperationHandler } from '../../../../../../shared/models/ihandable-component-interface/Ioperation-handler-interface';
import { EventEmitter } from '@angular/core';

type HandlerDependencies = {
  notificationService: UserOperationNotificationService;
  refresh: EventEmitter<void>;
};

const createHandler =
  (dependencies: HandlerDependencies) =>
  (paramater: EducationPlan): void => {
    const message = new EducationPlanCreationNotification().buildMessage(
      paramater,
    );
    dependencies.refresh.emit();
    dependencies.notificationService.SetMessage = message;
    dependencies.notificationService.success();
  };

const createErrorHandler =
  (dependecies: HandlerDependencies) =>
  (error: HttpErrorResponse): Observable<never> => {
    dependecies.notificationService.SetMessage = error.error;
    dependecies.notificationService.failure();
    return new Observable();
  };

export const EducationPlanCreationHandler = (
  notificationService: UserOperationNotificationService,
  refresh: EventEmitter<void>,
): IOperationHandler<EducationPlan> => {
  const dependencies: HandlerDependencies = {
    notificationService: notificationService,
    refresh: refresh,
  };
  const handle = createHandler(dependencies);
  const handleError = createErrorHandler(dependencies);
  return { handle, handleError };
};
