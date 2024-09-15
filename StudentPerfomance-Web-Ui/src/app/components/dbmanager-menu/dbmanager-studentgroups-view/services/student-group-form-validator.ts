import { FormGroup } from '@angular/forms';
import { ValidationResult } from '../../../../models/validation-result';
import { IValidator } from '../../../../models/validator-interface';

export class StudentGroupFormValidator implements IValidator {
  private readonly _form: FormGroup;

  public constructor(form: FormGroup) {
    this._form = form;
  }

  public validate(): ValidationResult {
    let errorMessage = '';
    if (this.isFormControlFieldNullOrEmpty('groupName')) {
      errorMessage = this.appendErrorMessage(
        errorMessage,
        'Название группы пустое'
      );
      return new ValidationResult(errorMessage, true);
    }
    if (this.isFormControlFieldExcessMaxLength('groupName', 15)) {
      errorMessage = this.appendErrorMessage(
        errorMessage,
        'Название группы превышает 15 символов'
      );
      return new ValidationResult(errorMessage, true);
    }
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

  private isFormControlFieldExcessMaxLength(
    formControlName: string,
    length: number
  ): boolean {
    const value = this._form.value[formControlName];
    return value.length > length;
  }
}
