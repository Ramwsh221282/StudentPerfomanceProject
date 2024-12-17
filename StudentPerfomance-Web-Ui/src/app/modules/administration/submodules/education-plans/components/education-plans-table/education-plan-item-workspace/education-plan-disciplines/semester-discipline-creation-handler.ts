import { HttpErrorResponse } from '@angular/common/http';
import { UserOperationNotificationService } from '../../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { SemesterPlan } from '../../../../../semester-plans/models/semester-plan.interface';
import { SemesterDisciplineCreatedNotification } from './semester-discipline-created-notification';
import { Observable } from 'rxjs';
import { IOperationHandler } from '../../../../../../../../shared/models/ihandable-component-interface/Ioperation-handler-interface';

type HandlerDependencies = {
  notificationService: UserOperationNotificationService;
};

const createHandler =
  (dependencies: HandlerDependencies) =>
  (parameter: SemesterPlan): void => {
    dependencies.notificationService.SetMessage =
      new SemesterDisciplineCreatedNotification().buildMessage(parameter);
    dependencies.notificationService.success();
  };

const createErrorHandler =
  (dependencies: HandlerDependencies) =>
  (error: HttpErrorResponse): Observable<never> => {
    dependencies.notificationService.SetMessage = error.error;
    dependencies.notificationService.failure();
    return new Observable();
  };

export const SemesterPlanCreationHandler = (
  notificationService: UserOperationNotificationService,
): IOperationHandler<SemesterPlan> => {
  const dependecies: HandlerDependencies = {
    notificationService: notificationService,
  };
  const handle = createHandler(dependecies);
  const handleError = createErrorHandler(dependecies);
  return { handle, handleError };
};
