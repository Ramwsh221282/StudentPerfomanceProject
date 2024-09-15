import { INotificationMessageBuilder } from '../../../../shared-models/interfaces/inotificated-component-interface/inotification-message-builder';
import { StudentGroup } from '../../services/studentsGroup.interface';

export class StudentGroupUpdatedNotification
  implements INotificationMessageBuilder<StudentGroup>
{
  private readonly _groupPreivous: StudentGroup;

  public constructor(group: StudentGroup) {
    this._groupPreivous = group;
  }

  public buildMessage(parameter: StudentGroup): string {
    return `Название группы ${this._groupPreivous.groupName} было изменено на ${parameter.groupName}`;
  }
}
