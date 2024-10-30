import { INotificationMessageBuilder } from '../../../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';
import { Teacher } from '../../../../../teachers/models/teacher.interface';

export class TeacherRemoveNotification
  implements INotificationMessageBuilder<Teacher>
{
  public buildMessage(parameter: Teacher): string {
    return `Удалён преподаватель ${parameter.surname} ${parameter.name} ${parameter.patronymic} из ${parameter.department.acronymus}`;
  }
}
