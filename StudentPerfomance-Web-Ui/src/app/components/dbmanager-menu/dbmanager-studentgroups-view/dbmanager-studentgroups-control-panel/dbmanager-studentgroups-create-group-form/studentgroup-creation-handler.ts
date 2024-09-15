import { HttpErrorResponse } from '@angular/common/http';
import { IOperationHandler } from '../../../../shared-models/interfaces/ihandable-component-interface/Ioperation-handler-interface';
import { INotificatable } from '../../../../shared-models/interfaces/inotificated-component-interface/inotificatable.interface';
import { StudentGroup } from '../../services/studentsGroup.interface';
import { StudentGroupCreatedNotification } from './studentgroup-created-notification';
import { Observable } from 'rxjs';
import { StudentGroupsFacadeService } from '../../services/student-groups-facade.service';
import { UserOperationNotificationService } from '../../../../shared-services/user-notifications/user-operation-notification-service.service';

type HandlerDependencies = {
  facadeService: StudentGroupsFacadeService;
  notificatable: INotificatable;
  notificationService: UserOperationNotificationService;
};

const createHandler =
  (dependencies: HandlerDependencies) =>
  (parameter: StudentGroup): void => {
    const message = new StudentGroupCreatedNotification().buildMessage(
      parameter
    );
    dependencies.facadeService.fetchData();
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

export const StudentGroupCreationHandler = (
  facadeService: StudentGroupsFacadeService,
  notificatable: INotificatable,
  notificationService: UserOperationNotificationService
): IOperationHandler<StudentGroup> => {
  const dependencies: HandlerDependencies = {
    facadeService: facadeService,
    notificatable: notificatable,
    notificationService: notificationService,
  };
  const handle = createHandler(dependencies);
  const handleError = createErrorHandler(dependencies);
  return { handle, handleError };
};
