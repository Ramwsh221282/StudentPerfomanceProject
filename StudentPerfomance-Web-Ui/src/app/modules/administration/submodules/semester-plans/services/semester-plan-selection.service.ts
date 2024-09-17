import { Injectable } from '@angular/core';
import { SemesterPlan } from '../../../../../modules/administration/submodules/semester-plans/models/semester-plan.interface';
import { SemesterPlansModule } from '../semester-plans.module';

@Injectable({
  providedIn: 'any',
})
export class SemesterPlanSelectionService {
  private _selected: SemesterPlan = {} as SemesterPlan;
  private _copy: SemesterPlan = {} as SemesterPlan;

  public constructor() {}

  public get selected(): SemesterPlan {
    return this._selected;
  }

  public get selectedCopy(): SemesterPlan {
    return this._copy;
  }

  public set setSelection(plan: SemesterPlan) {
    this._selected = { ...plan };
    this._copy = { ...plan };
  }

  public clearSelection(): void {
    this._selected = {} as SemesterPlan;
    this._copy = {} as SemesterPlan;
  }
}
