import { INotificationMessageBuilder } from '../../../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';
import { Teacher } from '../../../../../teachers/models/teacher.interface';

export class TeacherCreationNotification
  implements INotificationMessageBuilder<Teacher>
{
  public buildMessage(parameter: Teacher): string {
    return `Добавлен преподаватель ${parameter.surname} ${parameter.name} ${parameter.thirdname} в кафедру ${parameter.department.name}`;
  }
}
