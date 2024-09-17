import { HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Department } from '../../models/departments.interface';
import { DepartmentCreatedNotificationBuilder } from './department-created-notification-builder';
import { DepartmentsFacadeService } from '../../services/departments-facade.service';
import { IOperationHandler } from '../../../../../../shared/models/ihandable-component-interface/Ioperation-handler-interface';
import { INotificatable } from '../../../../../../shared/models/inotificated-component-interface/inotificatable.interface';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';

type DepartmentHandlerDependencies = {
  facadeService: DepartmentsFacadeService;
  notificatable: INotificatable;
  notificationService: UserOperationNotificationService;
};

const createDepartmentHandler =
  (dependencies: DepartmentHandlerDependencies) =>
  (parameter: Department): void => {
    const message = new DepartmentCreatedNotificationBuilder().buildMessage(
      parameter
    );
    dependencies.facadeService.fetchData();
    dependencies.facadeService.refreshPagination();
    dependencies.notificationService.SetMessage = message;
    dependencies.notificatable.successModalState.turnOn();
  };

const handleDepartmentError =
  (dependencies: DepartmentHandlerDependencies) =>
  (error: HttpErrorResponse): Observable<never> => {
    dependencies.notificationService.SetMessage = error.error;
    dependencies.notificatable.failureModalState.turnOn();
    return new Observable();
  };

export const DepartmentCreationHandler = (
  facadeService: DepartmentsFacadeService,
  notificatable: INotificatable,
  notificationService: UserOperationNotificationService
): IOperationHandler<Department> => {
  const dependencies: DepartmentHandlerDependencies = {
    facadeService: facadeService,
    notificatable: notificatable,
    notificationService: notificationService,
  };
  const handle = createDepartmentHandler(dependencies);
  const handleError = handleDepartmentError(dependencies);
  return { handle, handleError };
};
