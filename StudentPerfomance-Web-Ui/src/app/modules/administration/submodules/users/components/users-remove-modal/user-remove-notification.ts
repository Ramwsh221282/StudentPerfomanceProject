import { INotificationMessageBuilder } from '../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';
import { UserRecord } from '../../services/user-table-element-interface';

export class UserRemoveNotification
  implements INotificationMessageBuilder<UserRecord>
{
  public buildMessage(parameter: UserRecord): string {
    return `Удалён пользователь:
	${parameter.surname}
	${parameter.name[0]}
	${
    parameter.thirdname == null || parameter.thirdname == undefined
      ? ''
      : parameter.thirdname[0]
  }
  ${parameter.role}`;
  }
}
