import { FormGroup } from '@angular/forms';
import { ValidationResult } from '../../../../models/validation-result';
import { IValidator } from '../../../../models/validator-interface';

export class MergeStudentGroupFormValidator implements IValidator {
  private readonly _form: FormGroup;

  public constructor(form: FormGroup) {
    this._form = form;
  }

  validate(): ValidationResult {
    let errorMessage = '';
    if (this.isFormControlFieldNullOrEmpty('groupNameA'))
      this.appendErrorMessage(errorMessage, 'Название группы А пустое');
    if (this.isFormControlFieldNullOrEmpty('groupNameB'))
      this.appendErrorMessage(errorMessage, 'Название группы B пустое');
    if (this.areGroupsEqual('groupNameA', 'groupNameB'))
      this.appendErrorMessage(errorMessage, 'Группы одинаковые');
    if (errorMessage.length > 0)
      return new ValidationResult(errorMessage, true);
    return new ValidationResult(errorMessage, false);
  }

  private appendErrorMessage(container: string, message: string): string {
    container += message;
    return container;
  }

  private isFormControlFieldNullOrEmpty(formControlName: string): boolean {
    const value = this._form.value[formControlName];
    return value == null || value == undefined || value.length == 0;
  }

  private areGroupsEqual(
    formControlNameA: string,
    formControlNameB: string
  ): boolean {
    return (
      this._form.value[formControlNameA] == this._form.value[formControlNameB]
    );
  }
}
