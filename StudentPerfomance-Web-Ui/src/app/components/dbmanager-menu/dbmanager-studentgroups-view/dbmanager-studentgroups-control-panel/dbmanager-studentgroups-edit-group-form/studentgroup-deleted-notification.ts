import { INotificationMessageBuilder } from '../../../../shared-models/interfaces/inotificated-component-interface/inotification-message-builder';
import { StudentGroup } from '../../services/studentsGroup.interface';

export class StudentGroupDeletedNotification
  implements INotificationMessageBuilder<StudentGroup>
{
  buildMessage(parameter: StudentGroup): string {
    return `Группа ${parameter.groupName} была успешно удалена.`;
  }
}
