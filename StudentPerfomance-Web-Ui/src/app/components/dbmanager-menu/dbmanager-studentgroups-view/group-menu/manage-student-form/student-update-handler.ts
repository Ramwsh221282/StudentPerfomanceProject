import { HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IOperationHandler } from '../../../../shared-models/interfaces/ihandable-component-interface/Ioperation-handler-interface';
import { INotificatable } from '../../../../shared-models/interfaces/inotificated-component-interface/inotificatable.interface';
import { Student } from '../../models/student.interface';
import { StudentUpdateNotificationBuilder } from './student-update-notification-builder';
import { FacadeStudentService } from '../services/facade-student.service';
import { UserOperationNotificationService } from '../../../../shared-services/user-notifications/user-operation-notification-service.service';

type HandlerDependencies = {
  oldStudent: Student;
  facadeService: FacadeStudentService;
  notificatable: INotificatable;
  notificationService: UserOperationNotificationService;
};

const createHandler =
  (dependencies: HandlerDependencies) =>
  (parameter: Student): void => {
    const message = new StudentUpdateNotificationBuilder(
      dependencies.oldStudent
    ).buildMessage(parameter);
    dependencies.facadeService.fetchData();
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

export const StudentUpdateHandler = (
  oldStudent: Student,
  facadeService: FacadeStudentService,
  notificatable: INotificatable,
  notificationService: UserOperationNotificationService
): IOperationHandler<Student> => {
  const dependencies: HandlerDependencies = {
    oldStudent: oldStudent,
    facadeService: facadeService,
    notificatable: notificatable,
    notificationService: notificationService,
  };
  const handle = createHandler(dependencies);
  const handleError = createErrorHandler(dependencies);
  return { handle, handleError };
};
