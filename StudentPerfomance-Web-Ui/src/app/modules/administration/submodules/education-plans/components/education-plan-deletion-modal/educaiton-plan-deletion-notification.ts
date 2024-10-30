import { INotificationMessageBuilder } from '../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';
import { EducationPlan } from '../../models/education-plan-interface';

export class EducationPlanDeletionNotification
  implements INotificationMessageBuilder<EducationPlan>
{
  public buildMessage(parameter: EducationPlan): string {
    return `Удален учебный план:
	 ${parameter.year} года
	 ${parameter.direction.name}
	 ${parameter.direction.code}
	 ${parameter.direction.type}`;
  }
}
