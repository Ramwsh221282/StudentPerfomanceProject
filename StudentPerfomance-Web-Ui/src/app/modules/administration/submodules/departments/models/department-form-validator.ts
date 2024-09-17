import { FormGroup } from '@angular/forms';
import { ValidationResult } from '../../../../../../models/validation-result';
import { IValidator } from '../../../../../../models/validator-interface';

export class DepartmentsFormValidator implements IValidator {
  private readonly _form: FormGroup;

  public constructor(form: FormGroup) {
    this._form = form;
  }

  public validate(): ValidationResult {
    let errorMessage = '';
    if (this.isDepartmentNameEmpty())
      errorMessage = this.appendErrorText(errorMessage, 'Имя пустое ');
    if (this.isInputExcessMaxLength())
      errorMessage = this.appendErrorText(
        errorMessage,
        'Превышает длину 50 символов'
      );
    if (errorMessage.length == 0) {
      return new ValidationResult(errorMessage, false);
    }
    return new ValidationResult(errorMessage, true);
  }

  private isDepartmentNameEmpty(): boolean {
    const departmentName = this._form.value['departmentName'];
    return (
      departmentName == null ||
      departmentName == undefined ||
      departmentName.length == 0
    );
  }

  private isInputExcessMaxLength(): boolean {
    const departmentName = this._form.value['departmentName'];
    if (this.isDepartmentNameEmpty() || departmentName.length <= 50)
      return false;
    return true;
  }

  private appendErrorText(container: string, message: string): string {
    container += message;
    return container;
  }
}
