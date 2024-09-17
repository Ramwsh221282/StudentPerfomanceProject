import { INotificationMessageBuilder } from '../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';
import { Semester } from '../../models/semester.interface';

export class SemesterDeletedNotification
  implements INotificationMessageBuilder<Semester>
{
  public buildMessage(parameter: Semester): string {
    return `Удалён семестр со следующими параметрами:
			\nНомер семестра: ${parameter.number}
			\nПривязан к группе: ${parameter.groupName}
			\nЧисло контрактов: ${parameter.contractsCount}`;
  }
}
