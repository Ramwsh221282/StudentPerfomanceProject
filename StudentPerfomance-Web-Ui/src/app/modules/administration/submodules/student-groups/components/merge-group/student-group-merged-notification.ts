import { INotificationMessageBuilder } from '../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';
import { StudentGroup } from '../../services/studentsGroup.interface';

export class StudentGroupMergedNotification
  implements INotificationMessageBuilder<StudentGroup>
{
  private readonly _mergedGroup: StudentGroup;

  public constructor(group: StudentGroup) {
    this._mergedGroup = group;
  }

  public buildMessage(parameter: StudentGroup): string {
    return `В группу ${parameter.groupName} перенеслись студенты группы ${this._mergedGroup.groupName}`;
  }
}
