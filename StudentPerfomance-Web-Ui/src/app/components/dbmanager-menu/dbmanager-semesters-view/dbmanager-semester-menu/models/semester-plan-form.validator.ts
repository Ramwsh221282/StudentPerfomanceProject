import { FormGroup } from '@angular/forms';
import { ValidationResult } from '../../../../../models/validation-result';
import { IValidator } from '../../../../../models/validator-interface';

export class SemesterPlanFormValidator implements IValidator {
  private readonly _form: FormGroup;

  public constructor(form: FormGroup) {
    this._form = form;
  }

  public validate(): ValidationResult {
    let errorMessage = '';
    if (this.isFormFieldNullOrEmty('disciplineName')) {
      errorMessage = this.appendErrorMessage(
        errorMessage,
        'Дисциплина была пустая'
      );
      return new ValidationResult(errorMessage, true);
    }
    return new ValidationResult(errorMessage, false);
  }

  private isFormFieldNullOrEmty(formControlName: string): boolean {
    const value = this._form.value[formControlName];
    return value == null || value == undefined || value.length == 0;
  }

  private appendErrorMessage(container: string, message: string): string {
    container += message;
    return container;
  }
}
