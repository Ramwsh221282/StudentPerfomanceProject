import { StringValueBuilder } from '../../../../../shared/models/form-value-builder/string-value-builder';
import { Teacher } from './teacher.interface';

export class TeacherBuilder {
  private readonly _teacher: Teacher;

  public constructor() {
    this._teacher = {} as Teacher;
  }

  public buildDefault(): Teacher {
    const builder: StringValueBuilder = new StringValueBuilder();
    this._teacher.name = builder.extractStringOrEmpty(this._teacher.name);
    this._teacher.surname = builder.extractStringOrEmpty(this._teacher.surname);
    this._teacher.patronymic = builder.extractStringOrEmpty(
      this._teacher.patronymic
    );
    this._teacher.state = builder.extractStringOrEmpty(this._teacher.state);
    this._teacher.jobTitle = builder.extractStringOrEmpty(
      this._teacher.jobTitle
    );
    return this._teacher;
  }
}
