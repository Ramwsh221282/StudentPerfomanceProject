import { FormControl, FormGroup, Validators } from '@angular/forms';
import { StudentFormValidator } from './student-form-validator';
import { Student } from '../../models/student.interface';
import { StudentGroup } from '../../services/studentsGroup.interface';
import { FormValueBuilder } from '../../../../shared-models/form-value-builder/form-value-builder';

export abstract class BaseStudentForm {
  protected title: string;
  protected isErrorHidden: boolean;
  protected errorMessage: string;
  protected form: FormGroup;
  protected abstract submit(): void;

  public constructor(title: string) {
    this.title = title;
    this.isErrorHidden = true;
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

  protected isFormValid(): boolean {
    const validator = new StudentFormValidator(this.form);
    const result = validator.validate();
    if (result.IsError) {
      this.isErrorHidden = false;
      this.errorMessage = result.Message;
      return false;
    }
    this.isErrorHidden = true;
    this.errorMessage = '';
    return true;
  }
}
