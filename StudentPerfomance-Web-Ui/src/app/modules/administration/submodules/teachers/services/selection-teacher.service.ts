import { Injectable } from '@angular/core';
import { Teacher } from '../models/teacher.interface';
import { TeachersModule } from '../teachers.module';

@Injectable({
  providedIn: 'any',
})
export class SelectionTeacherService {
  private _selected: Teacher = {} as Teacher;
  private _copy: Teacher = {} as Teacher;

  public constructor() {}

  public get selected(): Teacher {
    return this._selected;
  }

  public get copy(): Teacher {
    return this._copy;
  }

  public set SetSelection(teacher: Teacher) {
    this._selected = { ...teacher };
    this._copy = { ...teacher };
  }

  public refreshSelection(): void {
    this._selected = {} as Teacher;
    this._copy = {} as Teacher;
  }
}
