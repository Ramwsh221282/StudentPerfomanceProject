import { HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Student } from '../../models/student.interface';
import { INotificatable } from '../../../../shared-models/interfaces/inotificated-component-interface/inotificatable.interface';
import { StudentCreatedNotificationBuilder } from './student-created-notification-buiilder';
import { FacadeStudentService } from '../services/facade-student.service';
import { UserOperationNotificationService } from '../../../../shared-services/user-notifications/user-operation-notification-service.service';

type HandlerDependencies = {
  facadeService: FacadeStudentService;
  notificatable: INotificatable;
  notificationService: UserOperationNotificationService;
};

const createHandler =
  (dependencies: HandlerDependencies) =>
  (parameter: Student): void => {
    const message = new StudentCreatedNotificationBuilder().buildMessage(
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

export const StudentCreationHandler = (
  facadeService: FacadeStudentService,
  notificatable: INotificatable,
  notificationService: UserOperationNotificationService
) => {
  const dependencies: HandlerDependencies = {
    facadeService: facadeService,
    notificatable: notificatable,
    notificationService: notificationService,
  };
  const handle = createHandler(dependencies);
  const handleError = createErrorHandler(dependencies);
  return { handle, handleError };
};
