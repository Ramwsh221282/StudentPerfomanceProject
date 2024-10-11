import { HttpErrorResponse } from '@angular/common/http';
import { IFailureNotificatable } from '../../../../../../shared/models/interfaces/ifailure-notificatable';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { StudentGroupsFacadeService } from '../../services/student-groups-facade.service';
import { StudentGroup } from '../../services/studentsGroup.interface';
import { Observable } from 'rxjs';
import { IOperationHandler } from '../../../../../../shared/models/ihandable-component-interface/Ioperation-handler-interface';

type HandlerDependencies = {
  facadeService: StudentGroupsFacadeService;
  notificationService: UserOperationNotificationService;
  failure: IFailureNotificatable;
};

const createHandler =
  (dependencies: HandlerDependencies) =>
  (parameter: StudentGroup): void => {
    dependencies.facadeService.refreshPagination();
    dependencies.facadeService.fetchData();
  };

const createErrorHandler =
  (dependencies: HandlerDependencies) =>
  (error: HttpErrorResponse): Observable<never> => {
    dependencies.notificationService.SetMessage = error.error;
    dependencies.failure.notifyFailure();
    return new Observable();
  };

export const StudentGroupDeletionHandler = (
  facadeService: StudentGroupsFacadeService,
  notificationService: UserOperationNotificationService,
  failure: IFailureNotificatable
): IOperationHandler<StudentGroup> => {
  const dependencies: HandlerDependencies = {
    facadeService: facadeService,
    notificationService: notificationService,
    failure: failure,
  };
  const handle = createHandler(dependencies);
  const handleError = createErrorHandler(dependencies);
  return { handle, handleError };
};
