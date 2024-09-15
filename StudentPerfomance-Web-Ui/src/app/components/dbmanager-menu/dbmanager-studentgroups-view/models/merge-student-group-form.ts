import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BaseStudentGroupForm } from './base-student-group-form';
import { StudentGroup } from '../services/studentsGroup.interface';
import { MergeStudentGroupFormValidator } from './merge-student-group-form-validator';
import { FormValueBuilder } from '../../../shared-models/form-value-builder/form-value-builder';

export abstract class MergeStudentGroupForm extends BaseStudentGroupForm {
  protected override initForm(): void {
    this.form = new FormGroup({
      groupNameA: new FormControl(null, [Validators.required]),
      groupNameB: new FormControl(null, [Validators.required]),
    });
  }

  protected override isFormValid(): boolean {
    const validator = new MergeStudentGroupFormValidator(this.form);
    const result = validator.validate();
    if (result.IsError) {
      this.isErrorHidden = false;
      this.errorMessage = result.Message;
      return false;
    }
    return true;
  }

  protected createGroupFromForm(formControlName: string): StudentGroup {
    const builder = new FormValueBuilder(this.form);
    return {
      groupName: builder.extractStringOrDefault(formControlName),
    } as StudentGroup;
  }
}
