import { INotificationMessageBuilder } from '../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';
import { Teacher } from '../../models/teacher.interface';

export class TeacherDeletedNotification
  implements INotificationMessageBuilder<Teacher>
{
  public buildMessage(parameter: Teacher): string {
    return `Удалён преподаватель со следующими данными:
			\nИмя: ${parameter.name}
			\nФамилия: ${parameter.surname}
			\nОтчество: ${parameter.thirdname}
			\nКафедра: ${parameter.departmentName}`;
  }
}
