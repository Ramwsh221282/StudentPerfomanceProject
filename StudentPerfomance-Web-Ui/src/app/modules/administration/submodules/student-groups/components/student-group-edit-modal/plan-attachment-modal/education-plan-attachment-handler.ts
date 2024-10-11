import { HttpErrorResponse } from '@angular/common/http';
import { IFailureNotificatable } from '../../../../../../../shared/models/interfaces/ifailure-notificatable';
import { ISuccessNotificatable } from '../../../../../../../shared/models/interfaces/isuccess-notificatable';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { StudentGroup } from '../../../services/studentsGroup.interface';
import { EducationPlanAttachmentNotification } from './education-plan-attachment-notification';
import { Observable } from 'rxjs';
import { IOperationHandler } from '../../../../../../../shared/models/ihandable-component-interface/Ioperation-handler-interface';

type HandlerDependencies = {
  notificationService: UserOperationNotificationService;
  success: ISuccessNotificatable;
  failure: IFailureNotificatable;
};

const createHandler =
  (dependencies: HandlerDependencies) =>
  (parameter: StudentGroup): void => {
    const message = new EducationPlanAttachmentNotification().buildMessage(
      parameter
    );
    dependencies.notificationService.SetMessage = message;
    dependencies.success.notifySuccess();
  };

const createErrorHandler =
  (dependencies: HandlerDependencies) =>
  (error: HttpErrorResponse): Observable<never> => {
    dependencies.notificationService.SetMessage = error.error;
    dependencies.failure.notifyFailure();
    return new Observable();
  };

export const EducationPlanAttachmentHandler = (
  notificationService: UserOperationNotificationService,
  success: ISuccessNotificatable,
  failure: IFailureNotificatable
): IOperationHandler<StudentGroup> => {
  const dependencies: HandlerDependencies = {
    notificationService: notificationService,
    success: success,
    failure: failure,
  };
  const handle = createHandler(dependencies);
  const handleError = createErrorHandler(dependencies);
  return { handle, handleError };
};
