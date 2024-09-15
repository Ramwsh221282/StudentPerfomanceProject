import { FormGroup } from '@angular/forms';
import { ValidationResult } from '../../../../../models/validation-result';
import { IValidator } from '../../../../../models/validator-interface';

export class TeacherFormValidator implements IValidator {
  private readonly _form: FormGroup;

  public constructor(form: FormGroup) {
    this._form = form;
  }

  public validate(): ValidationResult {
    let errorMessage = '';
    if (this.isNameEmpty())
      errorMessage = this.appendErrorText(errorMessage, 'Имя пустое ');
    if (this.isSurnameEmpty())
      errorMessage = this.appendErrorText(errorMessage, 'Фамилия была пустая');
    if (errorMessage.length == 0)
      return new ValidationResult(errorMessage, false);
    return new ValidationResult(errorMessage, true);
  }

  private appendErrorText(container: string, message: string) {
    container += message;
    return container;
  }

  private isNameEmpty(): boolean {
    const name = this._form.value['name'];
    return name == null || name == undefined || name.length == 0;
  }

  private isSurnameEmpty(): boolean {
    const surname = this._form.value['surname'];
    return surname == null || surname == undefined || surname.length == 0;
  }
}
