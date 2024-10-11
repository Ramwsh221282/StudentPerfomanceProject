import { INotificationMessageBuilder } from '../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';
import { EducationDirection } from '../../models/education-direction-interface';

export class EducationDirectionDeleteNotification
  implements INotificationMessageBuilder<EducationDirection>
{
  buildMessage(parameter: EducationDirection): string {
    return `Удалено направление подготовки ${parameter.name} ${parameter.code} ${parameter.type}`;
  }
}
