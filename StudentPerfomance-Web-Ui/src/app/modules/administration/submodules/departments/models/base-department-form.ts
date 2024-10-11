import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Department } from './departments.interface';
import { FormValueBuilder } from '../../../../../shared/models/form-value-builder/form-value-builder';
import { ISubbmittable } from '../../../../../shared/models/interfaces/isubbmitable';

export abstract class DepartmentFormBase implements ISubbmittable {
  protected title: string;
  protected form: FormGroup;
  public abstract submit(): void;

  public constructor(title: string) {
    this.title = title;
  }

  protected initForm(): void {
    this.form = new FormGroup({
      departmentName: new FormControl(null, [Validators.required]),
    });
  }

  protected createDepartmentFromForm(): Department {
    const builder = new FormValueBuilder(this.form);
    return {
      name: builder.extractStringOrDefault('departmentName'),
    } as Department;
  }
}
