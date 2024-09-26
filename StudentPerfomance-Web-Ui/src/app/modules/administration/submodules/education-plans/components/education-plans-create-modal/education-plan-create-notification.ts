import { INotificationMessageBuilder } from '../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';
import { EducationPlan } from '../../models/education-plan-interface';

export class EducationPlanCreateNotification
  implements INotificationMessageBuilder<EducationPlan>
{
  buildMessage(parameter: EducationPlan): string {
    return `Добавлен учебный план:
Год: ${parameter.year}
Код направления: ${parameter.direction.code}
Название направления: ${parameter.direction.name}
Имя направления: ${parameter.direction.type}`;
  }
}
