import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Semester } from './semester.interface';
import { FormValueBuilder } from '../../../shared-models/form-value-builder/form-value-builder';

export abstract class BaseSemesterForm {
  protected title: string;
  protected form: FormGroup;
  protected abstract submit(): void;

  public constructor(title: string) {
    this.title = title;
  }

  protected initForm(): void {
    this.form = new FormGroup({
      groupName: new FormControl(null, [Validators.required]),
      number: new FormControl(null, [Validators.required]),
    });
  }

  protected createSemesterFromForm(): Semester {
    const builder = new FormValueBuilder(this.form);
    return {
      groupName: builder.extractStringOrDefault('groupName'),
      number: builder.extractNumberOrDefault('number'),
      contractsCount: 0,
    } as Semester;
  }
}
