import { HttpErrorResponse } from '@angular/common/http';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { EducationPlan } from '../../../models/education-plan-interface';
import { FacadeService } from '../../../services/facade.service';
import { EducationPlanDeletionNotification } from './education-plans-deletion-notification';
import { Observable } from 'rxjs';
import { IOperationHandler } from '../../../../../../../shared/models/ihandable-component-interface/Ioperation-handler-interface';
import { INotificatable } from '../../../../../../../shared/models/inotificated-component-interface/inotificatable.interface';

type HandlerDependencies = {
  facadeService: FacadeService;
  notificationService: UserOperationNotificationService;
  notificatable: INotificatable;
};

const createHandler =
  (dependencies: HandlerDependencies) =>
  (parameter: EducationPlan): void => {
    const message = new EducationPlanDeletionNotification().buildMessage(
      parameter
    );
    dependencies.notificationService.SetMessage = message;
    dependencies.facadeService.refreshPagination();
    dependencies.facadeService.fetch();
    dependencies.notificatable.successModalState.turnOn();
  };

const createErrorHandler =
  (dependencies: HandlerDependencies) =>
  (error: HttpErrorResponse): Observable<never> => {
    dependencies.notificationService.SetMessage = error.error;
    dependencies.notificatable.failureModalState.turnOn();
    return new Observable();
  };

export const EducationPlanDeletionHandler = (
  facadeService: FacadeService,
  notificationService: UserOperationNotificationService,
  notificatable: INotificatable
): IOperationHandler<EducationPlan> => {
  const dependencies: HandlerDependencies = {
    facadeService: facadeService,
    notificationService: notificationService,
    notificatable: notificatable,
  };
  const handle = createHandler(dependencies);
  const handleError = createErrorHandler(dependencies);
  return { handle, handleError };
};
