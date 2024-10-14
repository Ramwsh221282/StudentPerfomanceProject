import { INotificationMessageBuilder } from '../../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';
import { Department } from '../../../models/departments.interface';

export class DepartmentDeletionNotification
  implements INotificationMessageBuilder<Department>
{
  public buildMessage(parameter: Department): string {
    return `Удалена кафедра ${parameter.name} ${parameter.shortName}`;
  }
}
