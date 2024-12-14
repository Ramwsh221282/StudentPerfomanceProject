import { HttpErrorResponse } from '@angular/common/http';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { StudentGroupsFacadeService } from '../../services/student-groups-facade.service';
import { StudentGroup } from '../../services/studentsGroup.interface';
import { StudentGroupCreationNotification } from './student-group-creation-notification';
import { Observable } from 'rxjs';
import { IOperationHandler } from '../../../../../../shared/models/ihandable-component-interface/Ioperation-handler-interface';
import { EventEmitter } from '@angular/core';

type HandlerDependencies = {
  facadeService: StudentGroupsFacadeService;
  notificationService: UserOperationNotificationService;
  refreshRequested: EventEmitter<void>;
  successEmitter: EventEmitter<void>;
  failureEmitter: EventEmitter<void>;
};

const createHandler =
  (dependencies: HandlerDependencies) =>
  (parameter: StudentGroup): void => {
    dependencies.notificationService.SetMessage =
      new StudentGroupCreationNotification().buildMessage(parameter);
    dependencies.refreshRequested.emit();
    dependencies.successEmitter.emit();
  };

const createErrorHandler =
  (dependencies: HandlerDependencies) =>
  (error: HttpErrorResponse): Observable<never> => {
    dependencies.notificationService.SetMessage = error.error;
    dependencies.failureEmitter.emit();
    return new Observable();
  };

export const StudentGroupCreationHandler = (
  facadeService: StudentGroupsFacadeService,
  notificationService: UserOperationNotificationService,
  refreshRequested: EventEmitter<void>,
  successEmitter: EventEmitter<void>,
  failureEmitter: EventEmitter<void>,
): IOperationHandler<StudentGroup> => {
  const dependencies: HandlerDependencies = {
    facadeService: facadeService,
    notificationService: notificationService,
    refreshRequested: refreshRequested,
    successEmitter: successEmitter,
    failureEmitter: failureEmitter,
  };
  const handle = createHandler(dependencies);
  const handleError = createErrorHandler(dependencies);
  return { handle, handleError };
};
