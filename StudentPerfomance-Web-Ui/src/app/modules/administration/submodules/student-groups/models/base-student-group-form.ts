import { FormControl, FormGroup, Validators } from '@angular/forms';
import { StudentGroup } from '../services/studentsGroup.interface';

export abstract class BaseStudentGroupForm {
  protected title: string;
  protected form: FormGroup;
  protected abstract submit(): void;

  public constructor(title: string) {
    this.title = title;
  }

  protected initForm(): void {
    this.form = new FormGroup({
      groupName: new FormControl(null, [Validators.required]),
    });
  }

  protected createStudentGroupFromForm(): StudentGroup {
    return {
      groupName: this.form.value['groupName'],
      studentsCount: 0,
    } as StudentGroup;
  }
}
