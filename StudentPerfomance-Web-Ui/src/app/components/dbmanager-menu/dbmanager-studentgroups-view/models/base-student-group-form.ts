import { FormControl, FormGroup, Validators } from '@angular/forms';
import { StudentGroup } from '../services/studentsGroup.interface';
import { StudentGroupFormValidator } from '../services/student-group-form-validator';

export abstract class BaseStudentGroupForm {
  protected title: string;
  protected isErrorHidden: boolean;
  protected errorMessage: string;
  protected form: FormGroup;
  protected abstract submit(): void;

  public constructor(title: string) {
    this.title = title;
    this.isErrorHidden = true;
    this.errorMessage = '';
  }

  protected initForm(): void {
    this.form = new FormGroup({
      groupName: new FormControl(null, [Validators.required]),
    });
    this.errorMessage = '';
  }

  protected createStudentGroupFromForm(): StudentGroup {
    return {
      groupName: this.form.value['groupName'],
      studentsCount: 0,
    } as StudentGroup;
  }

  protected isFormValid(): boolean {
    const validator = new StudentGroupFormValidator(this.form);
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
