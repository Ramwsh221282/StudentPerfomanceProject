import { INotificationMessageBuilder } from '../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';

export class AssignmentSessionClosedNotification
  implements INotificationMessageBuilder<any>
{
  buildMessage(parameter: any): string {
    return `Контрольная неделя успешно закрыта`;
  }
}
