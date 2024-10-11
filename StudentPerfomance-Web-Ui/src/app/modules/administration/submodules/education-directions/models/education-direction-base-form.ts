import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FormValueBuilder } from '../../../../../shared/models/form-value-builder/form-value-builder';
import { EducationDirection } from './education-direction-interface';

export abstract class EducationDirectionBaseForm {
  protected title: string;
  protected form: FormGroup;

  protected abstract submit(): void;

  protected initForm(): void {
    this.form = new FormGroup({
      code: new FormControl(null, [Validators.required]),
      name: new FormControl(null, [Validators.required]),
      type: new FormControl(null, [Validators.required]),
    });
  }

  protected createEducationDirectionFromForm() {
    const builder = new FormValueBuilder(this.form);
    return {
      entityNumber: 0,
      code: builder.extractStringOrDefault('code'),
      name: builder.extractStringOrDefault('name'),
      type: builder.extractStringOrDefault('type'),
    } as EducationDirection;
  }
}
