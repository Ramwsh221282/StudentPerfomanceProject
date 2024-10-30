import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Department } from './departments.interface';
import { FormValueBuilder } from '../../../../../shared/models/form-value-builder/form-value-builder';
import { DepartmentShortNameBuilder } from './builders/department-short-name.builder';

export abstract class DepartmentFormBase {
  protected form: FormGroup;

  protected initForm(): void {
    this.form = new FormGroup({
      name: new FormControl(null, [Validators.required]),
    });
  }

  protected createDepartmentFromForm(): Department {
    const builder = new FormValueBuilder(this.form);
    const department: Department = {
      name: builder.extractStringOrDefault('name'),
    } as Department;
    const shortNameBuilder = new DepartmentShortNameBuilder(department);
    department.acronymus = shortNameBuilder.buildShortName();
    return department;
  }
}
