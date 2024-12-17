import { HttpErrorResponse } from '@angular/common/http';
import { EducationPlan } from '../../models/education-plan-interface';
import { Observable } from 'rxjs';
import { IOperationHandler } from '../../../../../../shared/models/ihandable-component-interface/Ioperation-handler-interface';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { EventEmitter } from '@angular/core';
import { EducationPlanDeletionNotification } from './educaiton-plan-deletion-notification';

type HandlerDependencies = {
  service: UserOperationNotificationService;
  refresh: EventEmitter<void>;
  visibility: EventEmitter<boolean>;
};

const createHandler =
  (dependencies: HandlerDependencies) =>
  (parameter: EducationPlan): void => {
    dependencies.refresh.emit();
    dependencies.service.SetMessage =
      new EducationPlanDeletionNotification().buildMessage(parameter);
    dependencies.service.success();
    dependencies.visibility.emit(false);
  };

const createErrorHandler =
  (dependencies: HandlerDependencies) =>
  (error: HttpErrorResponse): Observable<never> => {
    dependencies.service.SetMessage = error.error;
    dependencies.service.failure();
    dependencies.visibility.emit(false);
    return new Observable();
  };

export const EducationPlanDeletionHandler = (
  service: UserOperationNotificationService,
  refresh: EventEmitter<void>,
  visibility: EventEmitter<boolean>,
): IOperationHandler<EducationPlan> => {
  const dependencies: HandlerDependencies = {
    service: service,
    refresh: refresh,
    visibility: visibility,
  };
  const handle = createHandler(dependencies);
  const handleError = createErrorHandler(dependencies);
  return { handle, handleError };
};
