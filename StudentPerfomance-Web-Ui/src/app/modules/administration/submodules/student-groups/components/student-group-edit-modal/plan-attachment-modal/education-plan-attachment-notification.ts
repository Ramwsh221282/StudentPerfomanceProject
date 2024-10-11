import { INotificationMessageBuilder } from '../../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';
import { StudentGroup } from '../../../services/studentsGroup.interface';

export class EducationPlanAttachmentNotification
  implements INotificationMessageBuilder<StudentGroup>
{
  public buildMessage(parameter: StudentGroup): string {
    return `К группе ${parameter.name} закреплен учебный план ${parameter.plan.year} ${parameter.plan.direction.name} ${parameter.plan.direction.code} ${parameter.plan.direction.type}`;
  }
}
