import { INotificationMessageBuilder } from '../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';
import { UserRecord } from '../../services/user-table-element-interface';

export class UserCreationNotification
  implements INotificationMessageBuilder<UserRecord>
{
  public buildMessage(parameter: UserRecord): string {
    return `Добавлен пользователь:
	${parameter.surname}
	${parameter.name[0]}
	${
    parameter.patronymic == null || parameter.patronymic == undefined
      ? ''
      : parameter.patronymic[0]
  }
  ${parameter.role}`;
  }
}
