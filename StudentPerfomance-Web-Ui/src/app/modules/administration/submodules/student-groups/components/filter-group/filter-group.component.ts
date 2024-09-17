import { Component, OnInit } from '@angular/core';
import { BaseStudentGroupForm } from '../../models/base-student-group-form';
import { StudentGroupsFacadeService } from '../../services/student-groups-facade.service';
import { StudentGroup } from '../../services/studentsGroup.interface';

@Component({
  selector: 'app-filter-group',
  templateUrl: './filter-group.component.html',
  styleUrl: './filter-group.component.scss',
})
export class FilterGroupComponent
  extends BaseStudentGroupForm
  implements OnInit
{
  public constructor(
    private readonly _facadeService: StudentGroupsFacadeService
  ) {
    super('Фильтр данных');
  }

  public ngOnInit(): void {
    this.initForm();
  }

  protected override submit(): void {
    this.ngOnInit();
  }

  protected filter(group: StudentGroup): void {
    this._facadeService.filterData(group);
    this.submit();
  }

  protected cancel(): void {
    this._facadeService.fetchData();
    this.submit();
  }
}
