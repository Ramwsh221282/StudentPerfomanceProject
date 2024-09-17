import { Injectable } from '@angular/core';
import { Semester } from '../models/semester.interface';

@Injectable({
  providedIn: 'root',
})
export class SemesterSelectionService {
  private _selected: Semester;
  private _copy: Semester;

  public constructor() {
    this._selected = {} as Semester;
    this._copy = {} as Semester;
  }

  public get selected(): Semester {
    return this._selected;
  }

  public get copy(): Semester {
    return this._copy;
  }

  public set setSelection(semester: Semester) {
    this._selected = { ...semester };
    this._copy = { ...semester };
  }

  public refreshSelection() {
    this._selected = {} as Semester;
    this._copy = {} as Semester;
  }
}
