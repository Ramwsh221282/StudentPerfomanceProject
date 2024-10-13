import { INotificationMessageBuilder } from '../../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';
import { Student } from '../../../../students/models/student.interface';

export class StudentDeletionNotification
  implements INotificationMessageBuilder<Student>
{
  public buildMessage(parameter: Student): string {
    return `Удалён студент ${parameter.surname} ${parameter.name} ${parameter.thirdname} ${parameter.recordbook} из группы ${parameter.group.name}`;
  }
}
