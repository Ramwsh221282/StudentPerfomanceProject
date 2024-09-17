import { INotificationMessageBuilder } from '../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';
import { Department } from '../../models/departments.interface';

export class DepartmentDeletedNotification
  implements INotificationMessageBuilder<Department>
{
  buildMessage(parameter: Department): string {
    return `Кафедра ${parameter.name} была успешно удалена.`;
  }
}
