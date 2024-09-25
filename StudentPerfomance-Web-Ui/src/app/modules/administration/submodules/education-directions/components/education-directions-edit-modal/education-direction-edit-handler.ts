import { HttpErrorResponse } from '@angular/common/http';
import { INotificatable } from '../../../../../../shared/models/inotificated-component-interface/inotificatable.interface';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { EducationDirection } from '../../models/education-direction-interface';
import { FacadeService } from '../../services/facade.service';
import { EducationDirectionEditNotificationBulder } from './education-direction-edit-notification';
import { Observable } from 'rxjs';
import { IOperationHandler } from '../../../../../../shared/models/ihandable-component-interface/Ioperation-handler-interface';

type HandlerDependencies = {
  oldDirection: EducationDirection;
  facadeService: FacadeService;
  notificationService: UserOperationNotificationService;
  notificatable: INotificatable;
};

const createHandler =
  (dependecies: HandlerDependencies) =>
  (parameter: EducationDirection): void => {
    const message = new EducationDirectionEditNotificationBulder(
      dependecies.oldDirection
    ).buildMessage(parameter);
    dependecies.notificationService.SetMessage = message;
    dependecies.facadeService.fetch();
    dependecies.notificatable.successModalState.turnOn();
    dependecies.facadeService.refreshSelection();
  };

const createErrorHandler =
  (dependencies: HandlerDependencies) =>
  (error: HttpErrorResponse): Observable<never> => {
    dependencies.notificationService.SetMessage = error.error;
    dependencies.notificatable.failureModalState.turnOn();
    return new Observable();
  };

export const CreateEducationDirectionEditHandler = (
  oldDirection: EducationDirection,
  facadeService: FacadeService,
  notificationService: UserOperationNotificationService,
  notificatable: INotificatable
): IOperationHandler<EducationDirection> => {
  const dependecies: HandlerDependencies = {
    oldDirection: oldDirection,
    facadeService: facadeService,
    notificationService: notificationService,
    notificatable: notificatable,
  };
  const handle = createHandler(dependecies);
  const handleError = createErrorHandler(dependecies);
  dependecies.facadeService.refreshSelection();
  return { handle, handleError };
};
