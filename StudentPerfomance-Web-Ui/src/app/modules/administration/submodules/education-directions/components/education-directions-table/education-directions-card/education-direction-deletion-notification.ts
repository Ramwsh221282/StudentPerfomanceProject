import { INotificationMessageBuilder } from '../../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';
import { EducationDirection } from '../../../models/education-direction-interface';

export class EducationDirectionDeletionNotificationBuilder
  implements INotificationMessageBuilder<EducationDirection>
{
  buildMessage(parameter: EducationDirection): string {
    return `Удалено направление подготовки:
Код: ${parameter.code}
Название: ${parameter.name}
Тип: ${parameter.type}`;
  }
}
