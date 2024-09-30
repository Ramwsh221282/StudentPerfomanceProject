import { INotificationMessageBuilder } from '../../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';
import { EducationPlan } from '../../../models/education-plan-interface';

export class EducationPlanDeletionNotification
  implements INotificationMessageBuilder<EducationPlan>
{
  public buildMessage(parameter: EducationPlan): string {
    return `Удалён учебный план:
Год: ${parameter.year}
Код: ${parameter.direction.code}
Название направления: ${parameter.direction.name}
Тип направления: ${parameter.direction.type}`;
  }
}
