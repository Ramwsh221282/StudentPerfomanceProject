import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Teacher } from './teacher.interface';
import { Department } from '../../departments/models/departments.interface';
import { FormValueBuilder } from '../../../../../shared/models/form-value-builder/form-value-builder';

export abstract class BaseTeacherForm {
  protected form: FormGroup;

  protected initForm(): void {
    this.form = new FormGroup({
      name: new FormControl(null, [Validators.required]),
      surname: new FormControl(null, [Validators.required]),
      thirdname: new FormControl(null, [Validators.required]),
      workingCondition: new FormControl(null, [Validators.required]),
      jobTitle: new FormControl(null, [Validators.required]),
    });
  }

  protected createTeacherFromForm(department: Department): Teacher {
    const builder = new FormValueBuilder(this.form);
    const teacher: Teacher = {} as Teacher;
    teacher.name = builder.extractStringOrDefault('name');
    teacher.surname = builder.extractStringOrDefault('surname');
    teacher.patronymic = builder.extractStringOrDefault('thirdname');
    teacher.state = builder.extractStringOrDefault('workingCondition');
    teacher.jobTitle = builder.extractStringOrDefault('jobTitle');
    teacher.department = { ...department };
    return teacher;
  }
}
