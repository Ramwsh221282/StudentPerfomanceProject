import { Student } from '../../models/student.interface';
import { StudentGroup } from '../../services/studentsGroup.interface';
import { BaseStudentForm } from '../models/base-student-form';

export class SearchStudentForm extends BaseStudentForm {
  protected override submit(): void {
    this.initForm();
  }

  protected override createStudentFromForm(group: StudentGroup): Student {
    return {
      name: this.buildFromFormProperty('name'),
      surname: this.buildFromFormProperty('surname'),
      thirdname: this.buildFromFormProperty('thirdname'),
      state: this.buildFromFormProperty('state'),
      groupName: group.groupName,
      recordBook:
        this.buildFromFormProperty('recordBook') == ''
          ? 0
          : Number(this.buildFromFormProperty('recordBook')),
    } as Student;
  }

  public constructor() {
    super('Поиск студентов');
  }

  private buildFromFormProperty(propertyName: string) {
    if (this.form.value[propertyName] == null) return '';
    return (this.form.value[propertyName] as string).trim();
  }
}
