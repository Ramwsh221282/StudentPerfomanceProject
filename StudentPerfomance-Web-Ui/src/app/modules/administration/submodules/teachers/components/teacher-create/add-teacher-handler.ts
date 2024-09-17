import { HttpErrorResponse } from '@angular/common/http';
import { Teacher } from '../../models/teacher.interface';
import { Observable } from 'rxjs';
import { IOperationHandler } from '../../../../../../shared/models/ihandable-component-interface/Ioperation-handler-interface';
import { INotificatable } from '../../../../../../shared/models/inotificated-component-interface/inotificatable.interface';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { FacadeTeacherService } from '../../services/facade-teacher.service';
import { TeacherAddedNotification } from './add-teacher-notification';

type HandlerDependencies = {
  facadeService: FacadeTeacherService;
  notificatable: INotificatable;
  notificationService: UserOperationNotificationService;
};

const createHandler =
  (dependencies: HandlerDependencies) =>
  (parameter: Teacher): void => {
    const message = new TeacherAddedNotification().buildMessage(parameter);
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

export const TeacherAddedHandler = (
  facadeService: FacadeTeacherService,
  notificatable: INotificatable,
  notificationService: UserOperationNotificationService
): IOperationHandler<Teacher> => {
  const dependencies: HandlerDependencies = {
    facadeService: facadeService,
    notificatable: notificatable,
    notificationService: notificationService,
  };
  const handle = createHandler(dependencies);
  const handleError = createErrorHandler(dependencies);
  return { handle, handleError };
};
