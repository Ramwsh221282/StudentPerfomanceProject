import { INotificationMessageBuilder } from '../../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';
import { Student } from '../../../../students/models/student.interface';

export class StudentEditNotification
  implements INotificationMessageBuilder<Student>
{
  private readonly _initial: Student;

  public constructor(initial: Student) {
    this._initial = initial;
  }

  public buildMessage(parameter: Student): string {
    return `Предыдущая информация студента:
	Фамилия: ${this._initial.surname}
	Имя: ${this._initial.name}
	Отчество: ${this._initial.patronymic}
	Зачетная книжка: ${this._initial.recordbook}
	Состояние: ${this._initial.state}
	Группа: ${this._initial.group.name}
	Новая информация:
	Фамилия: ${parameter.surname}
	Имя: ${parameter.name}
	Отчество: ${parameter.patronymic}
	Зачетная книжка: ${parameter.recordbook}
	Состояние: ${parameter.state}
	Группа: ${parameter.group.name}`;
  }
}
