import { Component, Input, OnInit } from '@angular/core';
import { SemesterPlanBaseForm } from '../models/semester-plan-base.form';
import { ReactiveFormsModule } from '@angular/forms';
import { Semester } from '../../models/semester.interface';
import { SemesterPlanFacadeService } from '../services/semester-plan-facade.service';

@Component({
  selector: 'app-semester-plan-search-form',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './semester-plan-search-form.component.html',
  styleUrl: './semester-plan-search-form.component.scss',
})
export class SemesterPlanSearchFormComponent
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
