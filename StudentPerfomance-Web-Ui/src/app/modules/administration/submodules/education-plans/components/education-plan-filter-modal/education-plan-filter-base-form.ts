import { FormControl, FormGroup, Validators } from '@angular/forms';
import { EducationPlan } from '../../models/education-plan-interface';
import { FormValueBuilder } from '../../../../../../shared/models/form-value-builder/form-value-builder';
import { EducationDirection } from '../../../education-directions/models/education-direction-interface';

export abstract class EducationPlanFilterBaseForm {
  protected readonly types: ['Бакалавриат', 'Магистратура'];
  protected readonly default: 'Тип направления';
  protected form: FormGroup;
  protected initForm(): void {
    this.form = new FormGroup({
      year: new FormControl('', [Validators.required]),
      name: new FormControl('', [Validators.required]),
      code: new FormControl('', [Validators.required]),
      type: new FormControl(this.types),
    });
    this.form.controls['type'].setValue(this.default);
  }

  protected createPlanFromForm(): EducationPlan {
    const builder: FormValueBuilder = new FormValueBuilder(this.form);
    const plan: EducationPlan = {} as EducationPlan;
    const direction: EducationDirection = {} as EducationDirection;
    plan.year = builder.extractNumberOrDefault('year');
    direction.name = builder.extractStringOrDefault('name');
    direction.code = builder.extractStringOrDefault('code');
    direction.type = builder.extractStringOrDefault('type');
    plan.direction = direction;
    return plan;
  }
}
