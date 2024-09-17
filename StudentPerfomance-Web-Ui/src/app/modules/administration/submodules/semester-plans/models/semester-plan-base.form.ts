import { FormControl, FormGroup, Validators } from '@angular/forms';
import { SemesterPlan } from './semester-plan.interface';
import { Semester } from '../../semesters/models/semester.interface';
import { FormValueBuilder } from '../../../../../shared/models/form-value-builder/form-value-builder';

export abstract class SemesterPlanBaseForm {
  protected title: string;
  protected form: FormGroup;
  protected abstract submit(): void;

  public constructor(title: string) {
    this.title = title;
  }

  protected initForm(): void {
    this.form = new FormGroup({
      disciplineName: new FormControl(null, [Validators.required]),
    });
  }

  protected createSemesterPlanFromForm(semester: Semester): SemesterPlan {
    const builder = new FormValueBuilder(this.form);
    return {
      semesterNumber: semester.number,
      groupName: semester.groupName,
      disciplineName: builder.extractStringOrDefault('disciplineName'),
    } as SemesterPlan;
  }
}
