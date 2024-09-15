import { UserOperationNotificationService } from '../../../shared-services/user-notifications/user-operation-notification-service.service';
import { INotificatable } from './inotificatable.interface';

export class NotificationManager {
  private readonly _service: UserOperationNotificationService;

  public constructor(service: UserOperationNotificationService) {
    this._service = service;
  }

  public ManageSuccess(message: string, notificatable: INotificatable) {
    notificatable.isSuccessVisible = true;
    notificatable.isFailureVisible = false;
    this._service.SetMessage = message;
  }

  public ManageError(message: string, notificatable: INotificatable) {
    notificatable.isFailureVisible = true;
    notificatable.isSuccessVisible = false;
    this._service.SetMessage = message;
  }
}
