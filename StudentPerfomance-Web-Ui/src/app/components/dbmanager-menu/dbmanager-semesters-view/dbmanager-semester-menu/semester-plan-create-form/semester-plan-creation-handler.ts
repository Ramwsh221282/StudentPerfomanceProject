import { HttpErrorResponse } from '@angular/common/http';
import { INotificatable } from '../../../../shared-models/interfaces/inotificated-component-interface/inotificatable.interface';
import { UserOperationNotificationService } from '../../../../shared-services/user-notifications/user-operation-notification-service.service';
import { SemesterPlan } from '../models/semester-plan.interface';
import { SemesterPlanFacadeService } from '../services/semester-plan-facade.service';
import { SemesterPlanCreatedNotification } from './semester-plan-created-notification';
import { Observable } from 'rxjs';
import { IOperationHandler } from '../../../../shared-models/interfaces/ihandable-component-interface/Ioperation-handler-interface';

type HandlerDependencies = {
  facadeService: SemesterPlanFacadeService;
  notificationService: UserOperationNotificationService;
  notificatable: INotificatable;
};

const createHandler =
  (dependencies: HandlerDependencies) =>
  (parameter: SemesterPlan): void => {
    const message = new SemesterPlanCreatedNotification().buildMessage(
      parameter
    );
    dependencies.facadeService.fetch();
    dependencies.facadeService.refreshPagination();
    dependencies.notificationService.SetMessage = message;
    dependencies.notificatable.successModalState.turnOn();
  };

const createErrorHandler =
  (dependencies: HandlerDependencies) =>
  (error: HttpErrorResponse): Observable<never> => {
    dependencies.notificationService.SetMessage = error.error;
    dependencies.notificatable.failureModalState.turnOn();
    return new Observable();
  };

export const SemesterPlanCreationHandler = (
  facadeService: SemesterPlanFacadeService,
  notificationService: UserOperationNotificationService,
  notificatable: INotificatable
): IOperationHandler<SemesterPlan> => {
  const dependencies: HandlerDependencies = {
    facadeService: facadeService,
    notificationService: notificationService,
    notificatable: notificatable,
  };
  const handle = createHandler(dependencies);
  const handleError = createErrorHandler(dependencies);
  return { handle, handleError };
};
