import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Student } from './student.interface';
import { FormValueBuilder } from '../../../../../shared/models/form-value-builder/form-value-builder';
import { StudentGroup } from '../../student-groups/services/studentsGroup.interface';

export abstract class BaseStudentForm {
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
      thirdname: new FormControl(null),
      recordBook: new FormControl(null, [Validators.required]),
      state: new FormControl(null, [Validators.required]),
    });
  }

  protected createStudentFromForm(group: StudentGroup): Student {
    const builder = new FormValueBuilder(this.form);
    return {
      name: builder.extractStringOrDefault('name'),
      surname: builder.extractStringOrDefault('surname'),
      thirdname: builder.extractStringOrDefault('thirdname'),
      recordBook: builder.extractNumberOrDefault('recordBook'),
      state: builder.extractStringOrDefault('state'),
      groupName: group.groupName,
    } as Student;
  }
}
