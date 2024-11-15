import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { StudentBaseForm } from '../student-creation-modal/student-base-form';
import { ISubbmittable } from '../../../../../../../shared/models/interfaces/isubbmitable';
import { groupStudentsDataService } from '../services/group-students-data.service';
import { StudentGroup } from '../../../services/studentsGroup.interface';
import { FilterFetchPolicy } from '../services/fetch-policies/filter-fetch-policy';
import { Student } from '../../../../students/models/student.interface';
import { AuthService } from '../../../../../../users/services/auth.service';
import { DefaultFetchPolicy } from '../services/fetch-policies/default-fetch-policy';

@Component({
  selector: 'app-student-filter-modal',
  templateUrl: './student-filter-modal.component.html',
  styleUrl: './student-filter-modal.component.scss',
})
export class StudentFilterModalComponent
  extends StudentBaseForm
  implements ISubbmittable, OnInit
{
  @Input({ required: true }) group: StudentGroup;
  @Output() visibility: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output() filterResult: EventEmitter<Student[]> = new EventEmitter<
    Student[]
  >();

  protected selectedState: string;

  public constructor(
    private readonly _service: groupStudentsDataService,
    private readonly _authService: AuthService,
  ) {
    super();
  }

  public ngOnInit(): void {
    this.initForm();
    this.selectedState = 'Параметр состояние не выбран';
  }

  public submit(): void {
    const student = this.createStudentFromForm(this.group);
    if (this.selectedState == 'Параметр состояние не выбран')
      this.selectedState = '';
    student.state = this.selectedState;

    this._service.setPolicy(new FilterFetchPolicy(student, this._authService));
    this._service.fetch().subscribe((response) => {
      this.filterResult.emit(response);
      this.close();
    });
  }

  protected cancelFilter(): void {
    this._service.setPolicy(
      new DefaultFetchPolicy(this.group, this._authService),
    );
    this._service.fetch().subscribe((response) => {
      this.filterResult.emit(response);
      this.close();
    });
  }

  protected close(): void {
    this.visibility.emit(false);
  }

  protected selectState(state: string): void {
    this.selectedState = state;
  }
}
