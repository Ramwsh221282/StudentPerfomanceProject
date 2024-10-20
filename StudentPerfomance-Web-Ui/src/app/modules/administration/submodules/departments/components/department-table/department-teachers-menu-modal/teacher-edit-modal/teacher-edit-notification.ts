import { INotificationMessageBuilder } from '../../../../../../../../shared/models/inotificated-component-interface/inotification-message-builder';
import { Teacher } from '../../../../../teachers/models/teacher.interface';

export class TeacherEditNotification
  implements INotificationMessageBuilder<Teacher>
{
  private readonly _teacher: Teacher;

  public constructor(teacher: Teacher) {
    this._teacher = teacher;
  }

  public buildMessage(parameter: Teacher): string {
    return `Предыдущая информация:
	Имя: ${this._teacher.name}
	Фамилия: ${this._teacher.surname}
	Отчество: ${this._teacher.thirdname}
	Условие работы: ${this._teacher.workingCondition}
	Должность: ${this._teacher.jobTitle}
	Новая информация:
	Имя: ${parameter.name}
	Фамилия: ${parameter.surname}
	Отчество: ${parameter.thirdname}
	Условие работы: ${parameter.workingCondition}
	Должность: ${parameter.jobTitle}`;
  }
}
