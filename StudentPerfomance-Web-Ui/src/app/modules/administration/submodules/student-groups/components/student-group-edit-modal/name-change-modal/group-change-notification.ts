import { INotificationMessageBuilder } from '../../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';
import { StudentGroup } from '../../../services/studentsGroup.interface';

export class GroupNameChangeNotification
  implements INotificationMessageBuilder<StudentGroup>
{
  private readonly _initial: StudentGroup;

  public constructor(initial: StudentGroup) {
    this._initial = initial;
  }

  public buildMessage(parameter: StudentGroup): string {
    return `Название группы ${this._initial.name} изменено на ${parameter.name}. Изменения в таблице появятся при закрытии окна редактирования`;
  }
}
