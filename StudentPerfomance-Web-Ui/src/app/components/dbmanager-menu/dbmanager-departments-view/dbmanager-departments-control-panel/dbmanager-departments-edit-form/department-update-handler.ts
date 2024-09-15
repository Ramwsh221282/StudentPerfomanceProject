import { HttpErrorResponse } from '@angular/common/http';
import { INotificatable } from '../../../../shared-models/interfaces/inotificated-component-interface/inotificatable.interface';
import { DepartmentsFacadeService } from '../../services/departments-facade.service';
import { Department } from '../../services/departments.interface';
import { DepartmentUpdatedNotification } from './department-updated-notification';
import { Observable } from 'rxjs';
import { IOperationHandler } from '../../../../shared-models/interfaces/ihandable-component-interface/Ioperation-handler-interface';
import { UserOperationNotificationService } from '../../../../shared-services/user-notifications/user-operation-notification-service.service';

type HandlerDependencies = {
  facadeService: DepartmentsFacadeService;
  notificatable: INotificatable;
  notificationService: UserOperationNotificationService;
};

const handler =
  (dependencies: HandlerDependencies) =>
  (parameter: Department): void => {
    const message = new DepartmentUpdatedNotification(
      dependencies.facadeService.selectedCopy
    ).buildMessage(parameter);
    dependencies.facadeService.fetchData();
    dependencies.facadeService.refreshSelection();
    dependencies.notificationService.SetMessage = message;
    dependencies.notificatable.successModalState.turnOn();
  };

const errorHandler =
  (dependencies: HandlerDependencies) =>
  (error: HttpErrorResponse): Observable<never> => {
    dependencies.facadeService.refreshSelection();
    dependencies.notificationService.SetMessage = error.error;
    dependencies.notificatable.failureModalState.turnOn();
    return new Observable();
  };

export const DepartmentUpdateHandler = (
  facadeService: DepartmentsFacadeService,
  notificatable: INotificatable,
  notificationService: UserOperationNotificationService
): IOperationHandler<Department> => {
  const dependencies: HandlerDependencies = {
    facadeService: facadeService,
    notificatable: notificatable,
    notificationService: notificationService,
  };
  const handle = handler(dependencies);
  const handleError = errorHandler(dependencies);
  return { handle, handleError };
};
