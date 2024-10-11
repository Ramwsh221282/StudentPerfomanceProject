import { HttpErrorResponse } from '@angular/common/http';
import { IFailureNotificatable } from '../../../../../../../../../shared/models/interfaces/ifailure-notificatable';
import { ISuccessNotificatable } from '../../../../../../../../../shared/models/interfaces/isuccess-notificatable';
import { UserOperationNotificationService } from '../../../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { SemesterPlan } from '../../../../../../semester-plans/models/semester-plan.interface';
import { SemesterDisciplineDeletionNotification } from './semester-discipline-deletion-notification';
import { Observable } from 'rxjs';
import { IOperationHandler } from '../../../../../../../../../shared/models/ihandable-component-interface/Ioperation-handler-interface';

type HandlerDependencies = {
  messageService: UserOperationNotificationService;
  success: ISuccessNotificatable;
  failure: IFailureNotificatable;
};

const createHandler =
  (dependencies: HandlerDependencies) =>
  (parameter: SemesterPlan): void => {
    const message = new SemesterDisciplineDeletionNotification().buildMessage(
      parameter
    );
    dependencies.messageService.SetMessage = message;
    dependencies.success.notifySuccess();
  };

const createErrorHandler =
  (dependencies: HandlerDependencies) =>
  (error: HttpErrorResponse): Observable<never> => {
    dependencies.messageService.SetMessage = error.error;
    dependencies.failure.notifyFailure();
    return new Observable();
  };

export const SemesterDisciplineDeletionHandler = (
  messageService: UserOperationNotificationService,
  success: ISuccessNotificatable,
  failure: IFailureNotificatable
): IOperationHandler<SemesterPlan> => {
  const dependecies: HandlerDependencies = {
    messageService: messageService,
    success: success,
    failure: failure,
  };
  const handle = createHandler(dependecies);
  const handleError = createErrorHandler(dependecies);
  return { handle, handleError };
};
