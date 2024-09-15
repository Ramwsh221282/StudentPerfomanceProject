import { FormGroup } from '@angular/forms';
import { ValidationResult } from '../../../../../models/validation-result';
import { IValidator } from '../../../../../models/validator-interface';

export class SemesterFormValidator implements IValidator {
  private readonly _form: FormGroup;

  public constructor(form: FormGroup) {
    this._form = form;
  }

  public validate(): ValidationResult {
    let errorContainer = '';
    if (this.isGroupNameEmpty())
      errorContainer = this.appendErrorText(
        errorContainer,
        'Название группы пустое'
      );
    if (this.isNumberNotValid())
      errorContainer = this.appendErrorText(
        errorContainer,
        'Недопустимый номер семестра'
      );
    if (errorContainer.length == 0)
      return new ValidationResult(errorContainer, false);
    return new ValidationResult(errorContainer, true);
  }

  private isGroupNameEmpty(): boolean {
    const value = this._form.value['groupName'];
    return value == null || value == undefined || value.length == 0;
  }

  private isNumberNotValid(): boolean {
    const value = this._form.value['number'];
    return value < 1 || value > 10;
  }

  private appendErrorText(container: string, message: string): string {
    container += message;
    return container;
  }
}
