import { HttpErrorResponse } from '@angular/common/http';
import { INotificatable } from '../../../../../../../shared/models/inotificated-component-interface/inotificatable.interface';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { EducationDirection } from '../../../models/education-direction-interface';
import { FacadeService } from '../../../services/facade.service';
import { EducationDirectionDeletionNotificationBuilder } from './education-direction-deletion-notification';
import { Observable } from 'rxjs';
import { IOperationHandler } from '../../../../../../../shared/models/ihandable-component-interface/Ioperation-handler-interface';

type HandlerDependencies = {
  facadeService: FacadeService;
  notificationService: UserOperationNotificationService;
  notificatable: INotificatable;
};

const createHandler =
  (dependencies: HandlerDependencies) =>
  (parameter: EducationDirection): void => {
    const message =
      new EducationDirectionDeletionNotificationBuilder().buildMessage(
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
    const message = error.error;
    dependencies.notificationService.SetMessage = message;
    dependencies.notificatable.failureModalState.turnOn();
    return new Observable();
  };

export const EducationDirectionDeletionHandler = (
  facadeService: FacadeService,
  notificationService: UserOperationNotificationService,
  notificatable: INotificatable
): IOperationHandler<EducationDirection> => {
  const dependencies: HandlerDependencies = {
    facadeService: facadeService,
    notificationService: notificationService,
    notificatable: notificatable,
  };
  const handle = createHandler(dependencies);
  const handleError = createErrorHandler(dependencies);
  return { handle, handleError };
};
