import { INotificationMessageBuilder } from '../../../../shared-models/interfaces/inotificated-component-interface/inotification-message-builder';
import { Department } from '../../services/departments.interface';

export class DepartmentDeletedNotification
  implements INotificationMessageBuilder<Department>
{
  buildMessage(parameter: Department): string {
    return `Кафедра ${parameter.name} была успешно удалена.`;
  }
}
