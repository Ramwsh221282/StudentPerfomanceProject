import { Injectable } from '@angular/core';
import { Department } from './departments.interface';

@Injectable({
  providedIn: 'root',
})
export class DepartmentsSelectionService {
  private _selected: Department = {} as Department;
  private _copy: Department = {} as Department;
  public constructor() {}

  public get selected(): Department {
    return this._selected;
  }

  public get copy(): Department {
    return this._copy;
  }

  public set setSelection(department: Department) {
    this._selected = { ...department };
    this._copy = { ...department };
  }

  public refreshSelection(): void {
    this._selected = {} as Department;
    this._copy = {} as Department;
  }
}
