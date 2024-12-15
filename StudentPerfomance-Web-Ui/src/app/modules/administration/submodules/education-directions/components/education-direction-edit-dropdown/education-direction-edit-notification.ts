import { INotificationMessageBuilder } from '../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';
import { EducationDirection } from '../../models/education-direction-interface';

export class EducationDirectionEditNotificationBulder
  implements INotificationMessageBuilder<EducationDirection>
{
  private readonly _oldDirection: EducationDirection;

  public constructor(oldDirection: EducationDirection) {
    this._oldDirection = oldDirection;
  }
  buildMessage(parameter: EducationDirection): string {
    return `Изменено направление подготовки:
Предыдущая информация:
Код: ${this._oldDirection.code}
Название: ${this._oldDirection.name}
Тип: ${this._oldDirection.type}
Новая информация:
Код: ${parameter.code}
Название: ${parameter.name}
Тип: ${parameter.type}`;
  }
}
