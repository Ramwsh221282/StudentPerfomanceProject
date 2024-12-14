import { FormControl, FormGroup, Validators } from '@angular/forms';
import { EducationPlan } from './education-plan-interface';
import { FormValueBuilder } from '../../../../../shared/models/form-value-builder/form-value-builder';
import { ISubbmittable } from '../../../../../shared/models/interfaces/isubbmitable';

export abstract class EducationPlanBaseForm implements ISubbmittable {
  protected title: string;
  protected form: FormGroup;

  public abstract submit(): void;

  protected initForm(): void {
    this.form = new FormGroup({
      year: new FormControl(null, [Validators.required]),
    });
  }

  protected createEducationPlanFromForm(): EducationPlan {
    const builder = new FormValueBuilder(this.form);
    return {
      year: builder.extractNumberOrDefault('year'),
    } as EducationPlan;
  }
}
