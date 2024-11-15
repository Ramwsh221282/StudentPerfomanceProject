import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { BaseStudentGroupForm } from '../../models/base-student-group-form';
import { StudentGroupsFacadeService } from '../../services/student-groups-facade.service';
import { ISubbmittable } from '../../../../../../shared/models/interfaces/isubbmitable';
import { FilterFetchPolicy } from '../../models/fetch-policies/filter-fetch-policy';
import { AuthService } from '../../../../../users/services/auth.service';
import { DefaultFetchPolicy } from '../../models/fetch-policies/default-fetch-policy';

@Component({
  selector: 'app-student-group-filter-modal',
  templateUrl: './student-group-filter-modal.component.html',
  styleUrl: './student-group-filter-modal.component.scss',
})
export class StudentGroupFilterModalComponent
  extends BaseStudentGroupForm
  implements OnInit, ISubbmittable
{
  @Output() visibility: EventEmitter<void> = new EventEmitter();

  public constructor(
    private readonly facadeService: StudentGroupsFacadeService,
    private readonly authService: AuthService,
  ) {
    super();
  }

  public submit(): void {
    const group = this.createGroupFromForm();
    this.facadeService.setPolicy(
      new FilterFetchPolicy(group, this.authService),
    );
    this.facadeService.fetchData();
    this.visibility.emit();
  }

  public ngOnInit(): void {
    this.initForm();
  }

  protected cancelFilter(): void {
    this.facadeService.setPolicy(new DefaultFetchPolicy(this.authService));
    this.facadeService.fetchData();
    this.visibility.emit();
  }
}
