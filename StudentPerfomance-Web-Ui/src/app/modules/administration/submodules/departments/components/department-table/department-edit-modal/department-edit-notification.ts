import { INotificationMessageBuilder } from '../../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';
import { Department } from '../../../models/departments.interface';

export class DepartmentEditNotification
  implements INotificationMessageBuilder<Department>
{
  private readonly _department: Department;

  public constructor(department: Department) {
    this._department = department;
  }

  public buildMessage(parameter: Department): string {
    return `Изменены данные кафедры:
	Предыдущая информация:
	Полное название: ${this._department.name}
	Аббревиатура: ${this._department.acronymus}
	Новая информация:
	Полное название: ${parameter.name}
	Аббревиатура: ${parameter.acronymus}`;
  }
}
