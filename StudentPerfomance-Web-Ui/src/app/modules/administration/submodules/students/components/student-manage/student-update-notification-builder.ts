import { INotificationMessageBuilder } from '../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';
import { Student } from '../../models/student.interface';

export class StudentUpdateNotificationBuilder
  implements INotificationMessageBuilder<Student>
{
  private readonly _oldData: Student;

  public constructor(oldData: Student) {
    this._oldData = oldData;
  }

  buildMessage(parameter: Student): string {
    return `Прошлая информация:
			\nИмя: ${this._oldData.name}
			\nФамилия: ${this._oldData.surname}
			\nОтчество: ${this._oldData.thirdname}
			\nСостояние: ${this._oldData.state}
			\nЗачётная книжка: ${this._oldData.recordBook}
			\nГруппа: ${this._oldData.groupName}
			\n
			\nНовая информация
			\nИмя: ${parameter.name}
			\nФамилия: ${parameter.surname}
			\nОтчество: ${parameter.thirdname}
			\nСостояние: ${parameter.state}
			\nЗачётная книжка: ${parameter.recordBook}
			\nГруппа: ${parameter.groupName}`;
  }
}
