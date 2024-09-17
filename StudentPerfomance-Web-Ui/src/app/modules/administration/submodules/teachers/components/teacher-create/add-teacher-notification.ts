import { INotificationMessageBuilder } from '../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';
import { Teacher } from '../../models/teacher.interface';

export class TeacherAddedNotification
  implements INotificationMessageBuilder<Teacher>
{
  public buildMessage(parameter: Teacher): string {
    return `Создан преподаватель со следующими параметрами:
			\nИмя: ${parameter.name}
			\nФамилия: ${parameter.surname}
			\nОтчество: ${parameter.thirdname}
			\nКафедра: ${parameter.departmentName}`;
  }
}
