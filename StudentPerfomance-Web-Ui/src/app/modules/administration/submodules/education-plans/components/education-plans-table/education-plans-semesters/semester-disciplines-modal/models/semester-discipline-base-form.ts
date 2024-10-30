import { FormControl, FormGroup, Validators } from '@angular/forms';
import { SemesterPlan } from '../../../../../../semester-plans/models/semester-plan.interface';
import { FormValueBuilder } from '../../../../../../../../../shared/models/form-value-builder/form-value-builder';
import { Semester } from '../../../../../../semesters/models/semester.interface';

export class SemesterDisciplineBaseForm {
  protected form: FormGroup;

  protected initForm(): void {
    this.form = new FormGroup({
      name: new FormControl(null, [Validators.required]),
    });
  }

  protected createSemesterPlanFromForm(semester: Semester): SemesterPlan {
    const builder = new FormValueBuilder(this.form);
    const plan = {} as SemesterPlan;
    plan.discipline = builder.extractStringOrDefault('name');
    plan.semester = { ...semester };
    return plan;
  }
}
