import { FormGroup } from '@angular/forms';

export class FormValueBuilder {
  private readonly _form: FormGroup;

  public constructor(form: FormGroup) {
    this._form = form;
  }

  public extractStringOrDefault(formControlName: string): string {
    return this._form.value[formControlName] == null || undefined
      ? ''
      : (this._form.value[formControlName] as string).trim();
  }

  public extractNumberOrDefault(formControlName: string): number {
    return this._form.value[formControlName] == null || undefined
      ? 0
      : this._form.value[formControlName];
  }
}
