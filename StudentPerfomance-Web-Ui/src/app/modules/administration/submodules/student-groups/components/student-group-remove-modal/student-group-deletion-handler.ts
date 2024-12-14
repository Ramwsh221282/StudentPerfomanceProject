import { HttpErrorResponse } from '@angular/common/http';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { StudentGroupsFacadeService } from '../../services/student-groups-facade.service';
import { StudentGroup } from '../../services/studentsGroup.interface';
import { Observable } from 'rxjs';
import { IOperationHandler } from '../../../../../../shared/models/ihandable-component-interface/Ioperation-handler-interface';
import { EventEmitter } from '@angular/core';

type HandlerDependencies = {
  facadeService: StudentGroupsFacadeService;
  notificationService: UserOperationNotificationService;
  success: EventEmitter<void>;
  failure: EventEmitter<void>;
  refreshEmitter: EventEmitter<void>;
};

const createHandler =
  (dependencies: HandlerDependencies) =>
  (parameter: StudentGroup): void => {
    dependencies.notificationService.SetMessage = `Удалена группа ${parameter.name}`;
    dependencies.facadeService.refreshPagination();
    dependencies.facadeService.fetchData();
    dependencies.success.emit();
  };

const createErrorHandler =
  (dependencies: HandlerDependencies) =>
  (error: HttpErrorResponse): Observable<never> => {
    dependencies.notificationService.SetMessage = error.error;
    dependencies.failure.emit();
    return new Observable();
  };

export const StudentGroupDeletionHandler = (
  facadeService: StudentGroupsFacadeService,
  notificationService: UserOperationNotificationService,
  success: EventEmitter<void>,
  failure: EventEmitter<void>,
  refreshEmitter: EventEmitter<void>,
): IOperationHandler<StudentGroup> => {
  const dependencies: HandlerDependencies = {
    facadeService: facadeService,
    notificationService: notificationService,
    success: success,
    failure: failure,
    refreshEmitter: refreshEmitter,
  };
  const handle = createHandler(dependencies);
  const handleError = createErrorHandler(dependencies);
  return { handle, handleError };
};
