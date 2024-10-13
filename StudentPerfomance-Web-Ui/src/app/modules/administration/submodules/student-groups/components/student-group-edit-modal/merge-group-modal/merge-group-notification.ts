import { INotificationMessageBuilder } from '../../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';
import { StudentGroup } from '../../../services/studentsGroup.interface';

export class StudentGroupMergeNotification
  implements INotificationMessageBuilder<StudentGroup>
{
  private readonly targetGroup: StudentGroup;

  public constructor(group: StudentGroup) {
    this.targetGroup = group;
  }

  public buildMessage(parameter: StudentGroup): string {
    return `В группу ${parameter.name} добавлены студенты группы ${this.targetGroup.name}`;
  }
}
