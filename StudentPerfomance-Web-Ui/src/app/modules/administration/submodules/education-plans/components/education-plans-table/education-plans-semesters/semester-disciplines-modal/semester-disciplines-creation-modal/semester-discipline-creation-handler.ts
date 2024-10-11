import { HttpErrorResponse } from '@angular/common/http';
import { IFailureNotificatable } from '../../../../../../../../../shared/models/interfaces/ifailure-notificatable';
import { ISuccessNotificatable } from '../../../../../../../../../shared/models/interfaces/isuccess-notificatable';
import { UserOperationNotificationService } from '../../../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { SemesterPlan } from '../../../../../../semester-plans/models/semester-plan.interface';
import { SemesterDisciplinesCreationService } from '../semester-disciplines-create.service';
import { SemesterDisciplineCreatedNotification } from './semester-discipline-created-notification';
import { Observable } from 'rxjs';
import { IOperationHandler } from '../../../../../../../../../shared/models/ihandable-component-interface/Ioperation-handler-interface';

type HandlerDependencies = {
  creationService: SemesterDisciplinesCreationService;
  notificationService: UserOperationNotificationService;
  success: ISuccessNotificatable;
  failure: IFailureNotificatable;
};

const createHandler =
  (dependencies: HandlerDependencies) =>
  (parameter: SemesterPlan): void => {
    const message = new SemesterDisciplineCreatedNotification().buildMessage(
      parameter
    );
    dependencies.notificationService.SetMessage = message;
    dependencies.success.notifySuccess();
  };

const createErrorHandler =
  (dependencies: HandlerDependencies) =>
  (error: HttpErrorResponse): Observable<never> => {
    dependencies.notificationService.SetMessage = error.error;
    dependencies.failure.notifyFailure();
    return new Observable();
  };

export const SemesterPlanCreationHandler = (
  creationService: SemesterDisciplinesCreationService,
  notificationService: UserOperationNotificationService,
  success: ISuccessNotificatable,
  failure: IFailureNotificatable
): IOperationHandler<SemesterPlan> => {
  const dependecies: HandlerDependencies = {
    creationService: creationService,
    notificationService: notificationService,
    success: success,
    failure: failure,
  };
  const handle = createHandler(dependecies);
  const handleError = createErrorHandler(dependecies);
  return { handle, handleError };
};
