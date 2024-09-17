import { INotificationMessageBuilder } from '../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';
import { SemesterPlan } from '../../models/semester-plan.interface';

export class SemesterPlanDeletedNotification
  implements INotificationMessageBuilder<SemesterPlan>
{
  public buildMessage(parameter: SemesterPlan): string {
    return `Удалён план семестра со следующими параметрами:
			\nДисциплина: ${parameter.disciplineName}
			\nГруппа: ${parameter.groupName}
			\nНомер семестра ${parameter.semesterNumber}`;
  }
}
