import { Component, Input, OnInit } from '@angular/core';
import { SearchStudentForm } from './search-student-form';
import { FacadeStudentService } from '../../services/facade-student.service';
import { StudentGroup } from '../../../student-groups/services/studentsGroup.interface';

@Component({
  selector: 'app-student-filter',
  templateUrl: './student-filter.component.html',
  styleUrl: './student-filter.component.scss',
})
export class StudentFilterComponent
  extends SearchStudentForm
  implements OnInit
{
  @Input({ required: true }) public currentGroup: StudentGroup;
  public constructor(private readonly _facadeService: FacadeStudentService) {
    super();
  }
  public ngOnInit(): void {
    this.initForm();
  }

  protected override submit(): void {
    this.ngOnInit();
  }

  protected searchClick(): void {
    this._facadeService.filterData(
      this.createStudentFromForm(this.currentGroup)
    );
    this.ngOnInit();
  }

  protected cancelClick(): void {
    this._facadeService.fetchData();
    this.ngOnInit();
  }
}
