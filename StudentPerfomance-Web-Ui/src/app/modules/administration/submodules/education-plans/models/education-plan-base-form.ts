import { FormControl, FormGroup, Validators } from '@angular/forms';
import { EducationPlan } from './education-plan-interface';
import { FormValueBuilder } from '../../../../../shared/models/form-value-builder/form-value-builder';
import { EducationDirection } from '../../education-directions/models/education-direction-interface';

export abstract class EducationPlanBaseForm {
  protected title: string;
  protected form: FormGroup;
  protected abstract submit(): void;
  protected initForm(): void {
    this.form = new FormGroup({
      year: new FormControl(null, [Validators.required]),
      code: new FormControl(null, [Validators.required]),
      name: new FormControl(null, [Validators.required]),
      type: new FormControl(null, [Validators.required]),
    });
  }

  protected createEducationPlanFromForm(): EducationPlan {
    const builder = new FormValueBuilder(this.form);
    const direction: EducationDirection = {
      code: builder.extractStringOrDefault('code'),
      name: builder.extractStringOrDefault('name'),
      type: builder.extractStringOrDefault('type'),
    } as EducationDirection;
    return {
      year: builder.extractNumberOrDefault('year'),
      direction: direction,
    } as EducationPlan;
  }
}
