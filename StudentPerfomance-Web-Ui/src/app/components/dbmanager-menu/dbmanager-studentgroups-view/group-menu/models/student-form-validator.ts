import { FormGroup } from '@angular/forms';
import { ValidationResult } from '../../../../../models/validation-result';
import { IValidator } from '../../../../../models/validator-interface';

export class StudentFormValidator implements IValidator {
  private readonly form: FormGroup;

  public constructor(form: FormGroup) {
    this.form = form;
  }

  validate(): ValidationResult {
    let errorMessage = '';
    if (this.isNameEmpty())
      errorMessage = this.appendErrorText(errorMessage, 'Имя пустое ');
    if (this.isSurnameEmpty())
      errorMessage = this.appendErrorText(errorMessage, 'Фамилия пустая ');
    if (this.isRecordBookEmpty())
      errorMessage = this.appendErrorText(
        errorMessage,
        'Зачётная книжка пустая '
      );
    if (this.isStateEmpty())
      errorMessage = this.appendErrorText(errorMessage, 'Состояние пустое');
    if (errorMessage.length == 0)
      return new ValidationResult(errorMessage, false);
    return new ValidationResult(errorMessage, true);
  }

  private appendErrorText(textContainer: string, message: string): string {
    textContainer += message;
    return textContainer;
  }

  private isNameEmpty(): boolean {
    const field = this.form.value['name'];
    return field == null || field == undefined || field.length == 0;
  }

  private isSurnameEmpty(): boolean {
    const field = this.form.value['surname'];
    return field == null || field == undefined || field.length == 0;
  }

  private isRecordBookEmpty(): boolean {
    const field = this.form.value['surname'];
    return field == null || field == undefined || field.length == 0;
  }

  private isStateEmpty(): boolean {
    const field = this.form.value['state'];
    return field == null || field == undefined || field.length == 0;
  }
}
