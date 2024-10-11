import { INotificationMessageBuilder } from '../../../../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';
import { SemesterPlan } from '../../../../../../semester-plans/models/semester-plan.interface';

export class SemesterDisciplineCreatedNotification
  implements INotificationMessageBuilder<SemesterPlan>
{
  buildMessage(parameter: SemesterPlan): string {
    return `Добавлена дисциплина ${parameter.discipline}`;
  }
}
