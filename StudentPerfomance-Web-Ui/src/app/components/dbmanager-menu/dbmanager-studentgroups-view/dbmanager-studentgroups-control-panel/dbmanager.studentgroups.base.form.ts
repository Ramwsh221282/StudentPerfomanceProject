import { FormGroup } from '@angular/forms';

export abstract class DbmanagerStudentGroupsFormBase {
  protected abstract title: string;
  protected abstract form: FormGroup;
  protected abstract initForm(): void;
  protected abstract submit(): void;
  protected abstract getErrorMessage(formControlName: string): string;
}
