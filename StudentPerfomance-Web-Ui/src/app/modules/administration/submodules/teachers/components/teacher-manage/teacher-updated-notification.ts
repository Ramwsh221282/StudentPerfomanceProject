import { INotificationMessageBuilder } from '../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';
import { Teacher } from '../../models/teacher.interface';

export class TeacherUpdatedNotification
  implements INotificationMessageBuilder<Teacher>
{
  private readonly _oldData: Teacher;

  public constructor(oldData: Teacher) {
    this._oldData = oldData;
  }

  public buildMessage(parameter: Teacher): string {
    return `Данные преподавателя были изменены:
			\nПредыдущая информация:
			\nИмя: ${this._oldData.name}
			\nИмя: ${this._oldData.surname}
			\nИмя: ${this._oldData.thirdname}
			\nИзменённая информация:
			\nИмя: ${parameter.name}
			\nИмя: ${parameter.surname}
			\nИмя: ${parameter.thirdname}`;
  }
}
