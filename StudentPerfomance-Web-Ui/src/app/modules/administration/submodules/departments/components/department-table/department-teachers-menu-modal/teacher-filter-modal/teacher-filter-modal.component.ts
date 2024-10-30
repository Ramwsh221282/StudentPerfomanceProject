import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ISubbmittable } from '../../../../../../../../shared/models/interfaces/isubbmitable';
import { Department } from '../../../../models/departments.interface';
import { Teacher } from '../../../../../teachers/models/teacher.interface';
import { TeacherDataService } from '../teachers-data.service';
import { TeacherBuilder } from '../../../../../teachers/models/teacher-builder';
import { TeacherFilterFetchPolicy } from '../../../../../teachers/models/fething-policies/teacher-filter-fetch-policy';
import { DefaultTeacherFetchPolicy } from '../../../../../teachers/models/fething-policies/default-teachers-fetch-policy';
import { AuthService } from '../../../../../../../users/services/auth.service';

@Component({
  selector: 'app-teacher-filter-modal',
  templateUrl: './teacher-filter-modal.component.html',
  styleUrl: './teacher-filter-modal.component.scss',
})
export class TeacherFilterModalComponent implements ISubbmittable, OnInit {
  @Input({ required: true }) department: Department;
  @Output() visibilityEmitter: EventEmitter<boolean> = new EventEmitter();
  @Output() refreshEmitter: EventEmitter<void> = new EventEmitter();

  protected teacher: Teacher;

  public constructor(
    private readonly _dataService: TeacherDataService,
    private readonly _authService: AuthService
  ) {
    this.teacher = {} as Teacher;
  }

  public ngOnInit(): void {
    const builder: TeacherBuilder = new TeacherBuilder();
    this.teacher = { ...builder.buildDefault() };
  }

  public submit(): void {
    this.teacher.department = { ...this.department };
    const fetchPolicy = new TeacherFilterFetchPolicy(this.teacher);
    this._dataService.setPolicy(fetchPolicy);
    this.refreshEmitter.emit();
    this.visibilityEmitter.emit();
  }

  public cancelFilter(): void {
    const fetchPolicy = new DefaultTeacherFetchPolicy(
      this.department,
      this._authService
    );
    this._dataService.setPolicy(fetchPolicy);
    this.refreshEmitter.emit();
    this.visibilityEmitter.emit();
  }
}
