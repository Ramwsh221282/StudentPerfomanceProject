import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Department } from '../services/departments.interface';
import { FormValueBuilder } from '../../../shared-models/form-value-builder/form-value-builder';

export abstract class DepartmentFormBase {
  protected title: string;
  protected isErrorHidden: boolean;
  protected errorMessage: string;
  protected form: FormGroup;
  protected abstract submit(): void;

  public constructor(title: string) {
    this.title = title;
    this.isErrorHidden = false;
    this.errorMessage = '';
    this.initForm();
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
