import { INotificationMessageBuilder } from '../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';
import { SemesterPlan } from '../../models/semester-plan.interface';

export class SemesterPlanCreatedNotification
  implements INotificationMessageBuilder<SemesterPlan>
{
  public buildMessage(parameter: SemesterPlan): string {
    return `Создан план семестра со следующими параметрами:
				\nДисциплина: ${parameter.disciplineName}
				\nГруппа: ${parameter.groupName}
				\nНомер семестра: ${parameter.semesterNumber}`;
  }
}
