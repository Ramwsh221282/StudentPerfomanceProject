import { INotificationMessageBuilder } from '../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';
import { EducationDirection } from '../../models/education-direction-interface';

export class EducationCreatedNotificationBuilder
  implements INotificationMessageBuilder<EducationDirection>
{
  buildMessage(parameter: EducationDirection): string {
    return `Направление подготовки добавлено:
Код: ${parameter.code}
Название: ${parameter.name}
Тип: ${parameter.type}`;
  }
}
