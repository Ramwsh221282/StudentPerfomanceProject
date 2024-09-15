import { INotificationMessageBuilder } from '../../../../shared-models/interfaces/inotificated-component-interface/inotification-message-builder';
import { Semester } from '../../models/semester.interface';

export class SemesterCreatedNotification
  implements INotificationMessageBuilder<Semester>
{
  public buildMessage(parameter: Semester): string {
    return `Создан семестр со следующими параметрами:
			\nНомер семестра: ${parameter.number}
			\nПривязан к группе: ${parameter.groupName}`;
  }
}
