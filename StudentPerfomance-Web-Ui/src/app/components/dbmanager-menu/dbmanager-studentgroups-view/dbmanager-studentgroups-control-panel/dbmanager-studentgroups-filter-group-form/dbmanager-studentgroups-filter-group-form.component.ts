import { Component } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BaseStudentGroupForm } from '../../models/base-student-group-form';
import { StudentGroupsFacadeService } from '../../services/student-groups-facade.service';

@Component({
  selector: 'app-dbmanager-studentgroups-filter-group-form',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './dbmanager-studentgroups-filter-group-form.component.html',
  styleUrl: './dbmanager-studentgroups-filter-group-form.component.scss',
})
export class DbmanagerStudentgroupsFilterGroupFormComponent extends BaseStudentGroupForm {
  constructor(private readonly _facadeService: StudentGroupsFacadeService) {
    super('Поиск группы');
    this.initForm();
  }

  protected submit(): void {
    this.initForm();
  }

  protected search(): void {
    this._facadeService.filterData(this.createStudentGroupFromForm());
    this.submit();
  }

  protected cancel(): void {
    this._facadeService.fetchData();
    this.submit();
  }
}
