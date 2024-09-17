import { INotificationMessageBuilder } from '../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';
import { Student } from '../../models/student.interface';

export class StudentDeletedNotificationBuilder
  implements INotificationMessageBuilder<Student>
{
  buildMessage(parameter: Student): string {
    return `Удалён студент со следующими данными:
				\nИмя: ${parameter.name}
				\nФамилия: ${parameter.surname}
				\nОтчество: ${parameter.thirdname}
				\nСостояние: ${parameter.thirdname}
				\nЗачётная книжка ${parameter.recordBook}
				\nГруппа: ${parameter.groupName}`;
  }
}
