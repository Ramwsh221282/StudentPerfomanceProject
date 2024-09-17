import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BaseStudentGroupForm } from './base-student-group-form';
import { FormValueBuilder } from '../../../../../shared/models/form-value-builder/form-value-builder';
import { StudentGroup } from '../services/studentsGroup.interface';

export abstract class MergeStudentGroupForm extends BaseStudentGroupForm {
  protected override initForm(): void {
    this.form = new FormGroup({
      groupNameA: new FormControl(null, [Validators.required]),
      groupNameB: new FormControl(null, [Validators.required]),
    });
  }

  protected createGroupFromForm(formControlName: string): StudentGroup {
    const builder = new FormValueBuilder(this.form);
    return {
      groupName: builder.extractStringOrDefault(formControlName),
    } as StudentGroup;
  }
}
