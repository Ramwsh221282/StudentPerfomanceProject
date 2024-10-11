import { INotificationMessageBuilder } from '../../../../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';
import { SemesterPlan } from '../../../../../../semester-plans/models/semester-plan.interface';

export class SemesterDisciplineDeletionNotification
  implements INotificationMessageBuilder<SemesterPlan>
{
  buildMessage(parameter: SemesterPlan): string {
    return `Удалена дисциплина ${parameter.discipline}`;
  }
}
