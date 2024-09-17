import { HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Department } from '../../models/departments.interface';
import { DepartmentsFacadeService } from '../../services/departments-facade.service';
import { DepartmentDeletedNotification } from './department-deleted-notification';
import { IOperationHandler } from '../../../../../../shared/models/ihandable-component-interface/Ioperation-handler-interface';
import { INotificatable } from '../../../../../../shared/models/inotificated-component-interface/inotificatable.interface';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';

type HandlerDependencies = {
  facadeService: DepartmentsFacadeService;
  notificatable: INotificatable;
  notificationService: UserOperationNotificationService;
};

const deleteDepartmentHandler =
  (dependencies: HandlerDependencies) =>
  (parameter: Department): void => {
    const message = new DepartmentDeletedNotification().buildMessage(parameter);
    dependencies.facadeService.fetchData();
    dependencies.facadeService.refreshPagination();
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

export const DepartmentDeletionHandler = (
  facadeService: DepartmentsFacadeService,
  notificatable: INotificatable,
  notificationService: UserOperationNotificationService
): IOperationHandler<Department> => {
  const dependencies: HandlerDependencies = {
    facadeService: facadeService,
    notificatable: notificatable,
    notificationService,
  };
  const handle = deleteDepartmentHandler(dependencies);
  const handleError = errorHandler(dependencies);
  return { handle, handleError };
};
