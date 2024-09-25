import { Injectable } from '@angular/core';
import { EducationDirection } from '../models/education-direction-interface';

@Injectable({
  providedIn: 'any',
})
export class SelectionService {
  private _copy: EducationDirection = {} as EducationDirection;
  private _selected: EducationDirection = {} as EducationDirection;

  public refreshSelection(): void {
    this._copy = {} as EducationDirection;
    this._selected = {} as EducationDirection;
  }

  public set Select(direction: EducationDirection) {
    this._copy = { ...direction };
    this._selected = { ...direction };
  }

  public get Selected(): EducationDirection {
    return this._selected;
  }

  public get Copy(): EducationDirection {
    return this._copy;
  }
}
