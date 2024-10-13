import { FormControl, FormGroup, Validators } from '@angular/forms';
import { StudentGroup } from '../../../services/studentsGroup.interface';
import { Student } from '../../../../students/models/student.interface';
import { FormValueBuilder } from '../../../../../../../shared/models/form-value-builder/form-value-builder';

export class StudentBaseForm {
  protected form: FormGroup;

  protected initForm(): void {
    this.form = new FormGroup({
      surname: new FormControl(null, [Validators.required]),
      name: new FormControl(null, [Validators.required]),
      thirdname: new FormControl(null, [Validators.required]),
      recordbook: new FormControl(null, [Validators.required]),
    });
  }

  protected createStudentFromForm(group: StudentGroup): Student {
    const builder = new FormValueBuilder(this.form);
    const student: Student = {} as Student;
    student.surname = builder.extractStringOrDefault('surname');
    student.name = builder.extractStringOrDefault('name');
    student.thirdname = builder.extractStringOrDefault('thirdname');
    student.recordbook = builder.extractNumberOrDefault('recordbook');
    student.group = { ...group };
    return student;
  }
}
