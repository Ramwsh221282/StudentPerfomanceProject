import { INotificationMessageBuilder } from '../../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';
import { Student } from '../../../../students/models/student.interface';

export class StudentCreationNotification
  implements INotificationMessageBuilder<Student>
{
  public buildMessage(parameter: Student): string {
    return `Добавлен студент группы ${parameter.surname} ${parameter.name} ${parameter.patronymic} ${parameter.recordbook} ${parameter.group.name}`;
  }
}
