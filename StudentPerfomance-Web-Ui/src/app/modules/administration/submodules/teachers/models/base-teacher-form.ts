import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Teacher } from './teacher.interface';
import { Department } from '../../departments/models/departments.interface';
import { FormValueBuilder } from '../../../../../shared/models/form-value-builder/form-value-builder';

export abstract class BaseTeacherForm {
  protected title: string;
  protected form: FormGroup;
  protected abstract submit(): void;

  public constructor(title: string) {
    this.title = title;
    this.initForm();
  }

  protected initForm(): void {
    this.form = new FormGroup({
      name: new FormControl(null, [Validators.required]),
      surname: new FormControl(null, [Validators.required]),
      thirdname: new FormControl(null, [Validators.required]),
    });
  }

  protected createTeacherFromForm(department: Department): Teacher {
    const builder = new FormValueBuilder(this.form);
    return {
      name: builder.extractStringOrDefault('name'),
      surname: builder.extractStringOrDefault('surname'),
      thirdname: builder.extractStringOrDefault('thirdname'),
      departmentName: department.name,
    } as Teacher;
  }
}
