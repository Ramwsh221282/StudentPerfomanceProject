import { INotificationMessageBuilder } from '../../../../shared-models/interfaces/inotificated-component-interface/inotification-message-builder';
import { Student } from '../../models/student.interface';

export class StudentCreatedNotificationBuilder
  implements INotificationMessageBuilder<Student>
{
  buildMessage(parameter: Student): string {
    return `Создан студент со следующими данными:
			\nИмя: ${parameter.name}
			\nФамилия: ${parameter.surname}
			\nОтчество: ${parameter.thirdname}
			\nСостояние ${parameter.state}
			\nЗачётная книжка ${parameter.recordBook}
			\nГруппа: ${parameter.groupName}`;
  }
}
