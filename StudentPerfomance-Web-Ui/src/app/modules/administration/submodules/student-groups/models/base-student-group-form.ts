import { FormControl, FormGroup, Validators } from '@angular/forms';
import { StudentGroup } from '../services/studentsGroup.interface';
import { FormValueBuilder } from '../../../../../shared/models/form-value-builder/form-value-builder';

export abstract class BaseStudentGroupForm {
  protected form: FormGroup;

  protected initForm(): void {
    this.form = new FormGroup({
      name: new FormControl(null, [Validators.required]),
    });
  }

  protected createGroupFromForm(): StudentGroup {
    const builder = new FormValueBuilder(this.form);
    return {
      name: builder.extractStringOrDefault('name'),
    } as StudentGroup;
  }
}
