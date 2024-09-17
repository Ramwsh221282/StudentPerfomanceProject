import { INotificationMessageBuilder } from '../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';
import { Department } from '../../models/departments.interface';

export class DepartmentUpdatedNotification
  implements INotificationMessageBuilder<Department>
{
  private readonly _oldData: Department;

  public constructor(oldData: Department) {
    this._oldData = oldData;
  }

  public buildMessage(parameter: Department): string {
    return `Кафедра ${this._oldData.name} переименована в ${parameter.name}`;
  }
}
