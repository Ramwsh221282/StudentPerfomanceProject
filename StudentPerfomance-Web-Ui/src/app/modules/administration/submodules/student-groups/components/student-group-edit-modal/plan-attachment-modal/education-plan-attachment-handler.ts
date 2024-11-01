import { HttpErrorResponse } from '@angular/common/http';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { StudentGroup } from '../../../services/studentsGroup.interface';
import { EducationPlanAttachmentNotification } from './education-plan-attachment-notification';
import { Observable } from 'rxjs';
import { IOperationHandler } from '../../../../../../../shared/models/ihandable-component-interface/Ioperation-handler-interface';
import { EventEmitter } from '@angular/core';

type HandlerDependencies = {
  notificationService: UserOperationNotificationService;
  success: EventEmitter<void>;
  failure: EventEmitter<void>;
  visibility: EventEmitter<void>;
};

const createHandler =
  (dependencies: HandlerDependencies) =>
  (parameter: StudentGroup): void => {
    const message = new EducationPlanAttachmentNotification().buildMessage(
      parameter
    );
    dependencies.notificationService.SetMessage = message;
    dependencies.success.emit();
    dependencies.visibility.emit();
  };

const createErrorHandler =
  (dependencies: HandlerDependencies) =>
  (error: HttpErrorResponse): Observable<never> => {
    dependencies.notificationService.SetMessage = error.error;
    dependencies.failure.emit();
    dependencies.visibility.emit();
    return new Observable();
  };

export const EducationPlanAttachmentHandler = (
  notificationService: UserOperationNotificationService,
  success: EventEmitter<void>,
  failure: EventEmitter<void>,
  visibility: EventEmitter<void>
): IOperationHandler<StudentGroup> => {
  const dependencies: HandlerDependencies = {
    notificationService: notificationService,
    success: success,
    failure: failure,
    visibility: visibility,
  };
  const handle = createHandler(dependencies);
  const handleError = createErrorHandler(dependencies);
  return { handle, handleError };
};
