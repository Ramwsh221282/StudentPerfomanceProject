import { HttpErrorResponse } from '@angular/common/http';
import { INotificatable } from '../../../../../../shared/models/inotificated-component-interface/inotificatable.interface';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { EducationDirection } from '../../models/education-direction-interface';
import { FacadeService } from '../../services/facade.service';
import { EducationCreatedNotificationBuilder } from './education-directions-create-notification';
import { Observable } from 'rxjs';
import { IOperationHandler } from '../../../../../../shared/models/ihandable-component-interface/Ioperation-handler-interface';

type HandlerDependencies = {
  facadeService: FacadeService;
  notificatable: INotificatable;
  notificationService: UserOperationNotificationService;
};

const createHandler =
  (dependencies: HandlerDependencies) =>
  (parameter: EducationDirection): void => {
    const message = new EducationCreatedNotificationBuilder().buildMessage(
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

export const EducationDirectionCreationHandler = (
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
