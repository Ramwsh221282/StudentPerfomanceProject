import { HttpErrorResponse } from '@angular/common/http';
import { IFailureNotificatable } from '../../../../../../shared/models/interfaces/ifailure-notificatable';
import { ISuccessNotificatable } from '../../../../../../shared/models/interfaces/isuccess-notificatable';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { EducationPlan } from '../../models/education-plan-interface';
import { FacadeService } from '../../services/facade.service';
import { EducationPlanCreationNotification } from './education-plan-creation-notification';
import { Observable } from 'rxjs';
import { IOperationHandler } from '../../../../../../shared/models/ihandable-component-interface/Ioperation-handler-interface';

type HandlerDependencies = {
  facadeService: FacadeService;
  notificationService: UserOperationNotificationService;
  success: ISuccessNotificatable;
  failure: IFailureNotificatable;
};

const createHandler =
  (dependencies: HandlerDependencies) =>
  (paramater: EducationPlan): void => {
    const message = new EducationPlanCreationNotification().buildMessage(
      paramater
    );
    dependencies.notificationService.SetMessage = message;
    dependencies.facadeService.refreshPagination();
    dependencies.facadeService.fetch();
    dependencies.success.notifySuccess();
  };

const createErrorHandler =
  (dependecies: HandlerDependencies) =>
  (error: HttpErrorResponse): Observable<never> => {
    dependecies.notificationService.SetMessage = error.error;
    dependecies.failure.notifyFailure();
    return new Observable();
  };

export const EducationPlanCreationHandler = (
  facadeService: FacadeService,
  notificationService: UserOperationNotificationService,
  success: ISuccessNotificatable,
  failure: IFailureNotificatable
): IOperationHandler<EducationPlan> => {
  const dependencies: HandlerDependencies = {
    facadeService: facadeService,
    notificationService: notificationService,
    success: success,
    failure: failure,
  };
  const handle = createHandler(dependencies);
  const handleError = createErrorHandler(dependencies);
  return { handle, handleError };
};
