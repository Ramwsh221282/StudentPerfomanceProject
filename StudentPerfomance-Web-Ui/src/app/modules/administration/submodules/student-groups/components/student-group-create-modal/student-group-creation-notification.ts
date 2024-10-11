import { INotificationMessageBuilder } from '../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';
import { StudentGroup } from '../../services/studentsGroup.interface';

export class StudentGroupCreationNotification
  implements INotificationMessageBuilder<StudentGroup>
{
  public buildMessage(parameter: StudentGroup): string {
    return `Добавлена студенческая группа ${parameter.name}`;
  }
}
