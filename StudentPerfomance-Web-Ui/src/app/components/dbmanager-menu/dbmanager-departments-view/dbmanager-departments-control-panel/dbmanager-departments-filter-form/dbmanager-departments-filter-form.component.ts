import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { DepartmentFormBase } from '../../models/base-department-form';
import { DepartmentsFacadeService } from '../../services/departments-facade.service';

@Component({
  selector: 'app-dbmanager-departments-filter-form',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './dbmanager-departments-filter-form.component.html',
  styleUrl: './dbmanager-departments-filter-form.component.scss',
})
export class DbmanagerDepartmentsFilterFormComponent
  extends DepartmentFormBase
  implements OnInit
{
  public constructor(
    private readonly _facadeService: DepartmentsFacadeService
  ) {
    super('Поиск кафедры');
  }

  public ngOnInit(): void {
    this.initForm();
  }

  protected override submit(): void {
    this.ngOnInit();
  }

  protected searchClick(): void {
    this._facadeService.filterData(this.createDepartmentFromForm());
    this.submit();
  }

  protected cancelClick(): void {
    this._facadeService.fetchData();
    this.submit();
  }
}
