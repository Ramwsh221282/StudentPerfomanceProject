import { FormControl, FormGroup, Validators } from '@angular/forms';
import { SemesterPlan } from '../../../../../../semester-plans/models/semester-plan.interface';
import { FormValueBuilder } from '../../../../../../../../../shared/models/form-value-builder/form-value-builder';

export class SemesterDisciplineBaseForm {
  protected form: FormGroup;

  protected initForm(): void {
    this.form = new FormGroup({
      name: new FormControl(null, [Validators.required]),
    });
  }

  protected createSemesterPlanFromForm(): SemesterPlan {
    const builder = new FormValueBuilder(this.form);
    const plan = {} as SemesterPlan;
    plan.discipline = builder.extractStringOrDefault('name');
    return plan;
  }
}
