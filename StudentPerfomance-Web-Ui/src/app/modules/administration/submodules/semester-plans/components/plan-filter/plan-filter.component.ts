import { Component, Input, OnInit } from '@angular/core';
import { Semester } from '../../../semesters/models/semester.interface';
import { SemesterPlanFacadeService } from '../../services/semester-plan-facade.service';
import { SemesterPlanBaseForm } from '../../models/semester-plan-base.form';

@Component({
  selector: 'app-plan-filter',
  templateUrl: './plan-filter.component.html',
  styleUrl: './plan-filter.component.scss',
})
export class PlanFilterComponent
  extends SemesterPlanBaseForm
  implements OnInit
{
  @Input({ required: true }) public currentSemester: Semester;

  public constructor(
    private readonly _facadeService: SemesterPlanFacadeService
  ) {
    super('Поиск дисциплины');
  }

  public ngOnInit(): void {
    this.initForm();
  }

  protected override submit(): void {
    this.ngOnInit();
  }

  protected searchClick(): void {
    this._facadeService.filter(
      this.createSemesterPlanFromForm(this.currentSemester)
    );
    this.submit();
  }

  protected cancelClick(): void {
    this._facadeService.fetch();
    this.submit();
  }
}
