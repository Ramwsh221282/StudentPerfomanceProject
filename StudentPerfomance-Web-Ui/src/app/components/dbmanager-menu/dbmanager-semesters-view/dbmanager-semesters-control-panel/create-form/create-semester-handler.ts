import { HttpErrorResponse } from '@angular/common/http';
import { INotificatable } from '../../../../shared-models/interfaces/inotificated-component-interface/inotificatable.interface';
import { Semester } from '../../models/semester.interface';
import { SemesterCreatedNotification } from './create-semester-notification';
import { Observable } from 'rxjs';
import { IOperationHandler } from '../../../../shared-models/interfaces/ihandable-component-interface/Ioperation-handler-interface';
import { SemesterFacadeService } from '../../services/semester-facade.service';
import { UserOperationNotificationService } from '../../../../shared-services/user-notifications/user-operation-notification-service.service';

type HandlerDependencies = {
  facadeService: SemesterFacadeService;
  notificatable: INotificatable;
  notificationService: UserOperationNotificationService;
};

const createHandler =
  (dependencies: HandlerDependencies) =>
  (parameter: Semester): void => {
    const message = new SemesterCreatedNotification().buildMessage(parameter);
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

export const SemesterCreatedHandler = (
  facadeService: SemesterFacadeService,
  notificatable: INotificatable,
  notificationService: UserOperationNotificationService
): IOperationHandler<Semester> => {
  const dependencies: HandlerDependencies = {
    facadeService: facadeService,
    notificatable: notificatable,
    notificationService: notificationService,
  };
  const handle = createHandler(dependencies);
  const handleError = createErrorHandler(dependencies);
  return { handle, handleError };
};
