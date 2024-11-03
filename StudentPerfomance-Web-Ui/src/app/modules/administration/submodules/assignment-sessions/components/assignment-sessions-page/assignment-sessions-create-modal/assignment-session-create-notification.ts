import { INotificationMessageBuilder } from '../../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';
import { AssignmentSession } from '../../../models/assignment-session-interface';

export class AssignmentSessionCreatedNotification
  implements INotificationMessageBuilder<AssignmentSession>
{
  public buildMessage(parameter: AssignmentSession): string {
    return `Добавлена контрольная неделя ${parameter.startDate}-${parameter.endDate}`;
  }
}
