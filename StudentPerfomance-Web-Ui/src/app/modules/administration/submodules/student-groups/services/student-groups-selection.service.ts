import { Injectable } from '@angular/core';
import { StudentGroup } from './studentsGroup.interface';

@Injectable({
  providedIn: 'any',
})
export class StudentGroupsSelectionService {
  private _selectedGroup: StudentGroup = {} as StudentGroup;
  private _groupCopy: StudentGroup = {} as StudentGroup;

  constructor() {}

  public get selected(): StudentGroup {
    return this._selectedGroup;
  }

  public get copy(): StudentGroup {
    return this._groupCopy;
  }

  public set set(group: StudentGroup) {
    this._selectedGroup = { ...group };
    this._groupCopy = { ...group };
  }

  public clear(): void {
    this._selectedGroup = {} as StudentGroup;
    this._groupCopy = {} as StudentGroup;
  }
}
