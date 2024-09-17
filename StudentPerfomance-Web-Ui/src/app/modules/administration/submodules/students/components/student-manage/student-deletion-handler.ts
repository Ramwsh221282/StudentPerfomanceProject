import { HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Student } from '../../models/student.interface';
import { StudentCreatedNotificationBuilder } from '../student-create/student-created-notification-buiilder';
import { IOperationHandler } from '../../../../../../shared/models/ihandable-component-interface/Ioperation-handler-interface';
import { INotificatable } from '../../../../../../shared/models/inotificated-component-interface/inotificatable.interface';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { FacadeStudentService } from '../../services/facade-student.service';

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
    dependencies.facadeService.refreshSelection();
    dependencies.notificationService.SetMessage = message;
    dependencies.notificatable.successModalState.turnOn();
  };

const createErrorHandler =
  (dependencies: HandlerDependencies) =>
  (error: HttpErrorResponse): Observable<never> => {
    dependencies.facadeService.refreshSelection();
    dependencies.notificationService.SetMessage = error.error;
    dependencies.notificatable.failureModalState.turnOn();
    return new Observable();
  };

export const StudentDeletionHandler = (
  facadeService: FacadeStudentService,
  notificatable: INotificatable,
  notificationService: UserOperationNotificationService
): IOperationHandler<Student> => {
  const dependencies: HandlerDependencies = {
    facadeService: facadeService,
    notificatable: notificatable,
    notificationService: notificationService,
  };
  const handle = createHandler(dependencies);
  const handleError = createErrorHandler(dependencies);
  return { handle, handleError };
};
