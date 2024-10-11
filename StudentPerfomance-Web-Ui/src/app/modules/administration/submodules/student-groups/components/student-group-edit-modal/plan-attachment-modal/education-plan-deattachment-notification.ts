import { INotificationMessageBuilder } from '../../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';
import { StudentGroup } from '../../../services/studentsGroup.interface';

export class EducationPlanDeattachmentNotification
  implements INotificationMessageBuilder<StudentGroup>
{
  public buildMessage(parameter: StudentGroup): string {
    return `У группы ${parameter.name} откреплён учебный план`;
  }
}
