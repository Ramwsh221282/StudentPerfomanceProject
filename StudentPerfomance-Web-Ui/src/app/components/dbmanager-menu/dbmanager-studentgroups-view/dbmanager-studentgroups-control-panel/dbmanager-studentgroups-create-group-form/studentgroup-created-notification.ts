import { INotificationMessageBuilder } from '../../../../shared-models/interfaces/inotificated-component-interface/inotification-message-builder';
import { StudentGroup } from '../../services/studentsGroup.interface';

export class StudentGroupCreatedNotification
  implements INotificationMessageBuilder<StudentGroup>
{
  buildMessage(parameter: StudentGroup): string {
    return `Созданная группа: ${parameter.groupName}.\nКоличество студентов: ${parameter.studentsCount}`;
  }
}
