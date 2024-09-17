import { Component, OnInit } from '@angular/core';
import { DepartmentFormBase } from '../../models/base-department-form';
import { DepartmentsFacadeService } from '../../services/departments-facade.service';

@Component({
  selector: 'app-filter-department',
  templateUrl: './filter-department.component.html',
  styleUrl: './filter-department.component.scss',
})
export class FilterDepartmentComponent
  extends DepartmentFormBase
  implements OnInit
{
  public constructor(
    private readonly _facadeService: DepartmentsFacadeService
  ) {
    super('Фильтр кафедр');
  }

  public ngOnInit(): void {
    this.initForm();
  }

  protected override submit(): void {
    this.ngOnInit();
  }

  protected filterClick(): void {
    this._facadeService.filterData(this.createDepartmentFromForm());
    this.submit();
  }

  protected cancelClick(): void {
    this._facadeService.fetchData();
    this.submit();
  }
}
