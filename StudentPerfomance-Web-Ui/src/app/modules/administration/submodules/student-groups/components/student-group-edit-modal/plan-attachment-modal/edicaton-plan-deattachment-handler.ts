import { HttpErrorResponse } from '@angular/common/http';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { StudentGroup } from '../../../services/studentsGroup.interface';
import { EducationPlanDeattachmentNotification } from './education-plan-deattachment-notification';
import { Observable } from 'rxjs';
import { IOperationHandler } from '../../../../../../../shared/models/ihandable-component-interface/Ioperation-handler-interface';
import { EventEmitter } from '@angular/core';

type HandlerDependencies = {
  notificationService: UserOperationNotificationService;
  successEmitter: EventEmitter<void>;
  failureEmitter: EventEmitter<void>;
  refreshEmitter: EventEmitter<void>;
};

const createHandler =
  (dependencies: HandlerDependencies) =>
  (parameter: StudentGroup): void => {
    dependencies.notificationService.SetMessage =
      new EducationPlanDeattachmentNotification().buildMessage(parameter);
    dependencies.successEmitter.emit();
    dependencies.refreshEmitter.emit();
  };

const createErrorHandler =
  (dependencies: HandlerDependencies) =>
  (error: HttpErrorResponse): Observable<never> => {
    dependencies.notificationService.SetMessage = error.error;
    dependencies.failureEmitter.emit();
    return new Observable();
  };

export const EducationPlanDeattachmentHandler = (
  notificationService: UserOperationNotificationService,
  successEmitter: EventEmitter<void>,
  failureEmitter: EventEmitter<void>,
  refreshEmitter: EventEmitter<void>,
): IOperationHandler<StudentGroup> => {
  const dependencies: HandlerDependencies = {
    notificationService: notificationService,
    successEmitter: successEmitter,
    failureEmitter: failureEmitter,
    refreshEmitter: refreshEmitter,
  };
  const handle = createHandler(dependencies);
  const handleError = createErrorHandler(dependencies);
  return { handle, handleError };
};
