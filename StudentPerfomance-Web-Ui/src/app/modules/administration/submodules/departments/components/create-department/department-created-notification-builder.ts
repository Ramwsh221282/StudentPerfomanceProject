import { INotificationMessageBuilder } from '../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';
import { Department } from '../../models/departments.interface';

export class DepartmentCreatedNotificationBuilder
  implements INotificationMessageBuilder<Department>
{
  buildMessage(parameter: Department): string {
    return `Кафедра: ${parameter.name} создана.\nЧисло преподавателей: ${parameter.teachersCount}.`;
  }
}
