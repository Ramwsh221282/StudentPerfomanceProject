import { Injectable } from '@angular/core';
import { StudentsModule } from '../students.module';
import { Student } from '../models/student.interface';

@Injectable({
  providedIn: 'any',
})
export class SelectionStudentService {
  private _selected: Student = {} as Student;
  private _copy: Student = {} as Student;

  public constructor() {}

  public get selected(): Student {
    return this._selected;
  }

  public get copy(): Student {
    return this._copy;
  }

  public set set(student: Student) {
    this._selected = { ...student };
    this._copy = { ...student };
  }

  public clear(): void {
    this._selected = {} as Student;
    this._copy = {} as Student;
  }
}
