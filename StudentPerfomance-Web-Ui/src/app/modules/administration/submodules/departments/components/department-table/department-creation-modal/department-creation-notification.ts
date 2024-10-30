import { INotificationMessageBuilder } from '../../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';
import { Department } from '../../../models/departments.interface';

export class DepartmentCreationNotification
  implements INotificationMessageBuilder<Department>
{
  public buildMessage(parameter: Department): string {
    return `Создана кафедра ${parameter.name} ${parameter.acronymus}`;
  }
}
