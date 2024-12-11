import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Department } from '../../../models/departments.interface';
import { Teacher } from '../../../../teachers/models/teacher.interface';
import { TeacherDataService } from './teachers-data.service';
import { DefaultTeacherFetchPolicy } from '../../../../teachers/models/fething-policies/default-teachers-fetch-policy';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { AuthService } from '../../../../../../users/services/auth.service';
import { AppConfigService } from '../../../../../../../app.config.service';

@Component({
  selector: 'app-department-teachers-menu-modal',
  templateUrl: './department-teachers-menu-modal.component.html',
  styleUrl: './department-teachers-menu-modal.component.scss',
  providers: [TeacherDataService, UserOperationNotificationService],
})
export class DepartmentTeachersMenuModalComponent implements OnInit {
  @Input({ required: true }) department: Department;
  @Output() visibilityEmitter: EventEmitter<boolean> =
    new EventEmitter<boolean>();

  protected isSuccess: boolean;
  protected isFailure: boolean;

  protected filterModalVisibility: boolean;
  protected creationModalVisibility: boolean;
  protected deletionModalVisibility: boolean;
  protected editModalVisibility: boolean;

  protected activeTeacher: Teacher;
  protected teachers: Teacher[];

  public constructor(
    protected readonly notificationService: UserOperationNotificationService,
    private readonly _dataService: TeacherDataService,
    private readonly _authService: AuthService,
    private readonly _appConfig: AppConfigService,
  ) {
    this.activeTeacher = {} as Teacher;
    this.teachers = [];
    this.creationModalVisibility = false;
    this.deletionModalVisibility = false;
    this.editModalVisibility = false;
    this.isSuccess = false;
    this.isFailure = false;
  }

  public ngOnInit(): void {
    this._dataService.setPolicy(
      new DefaultTeacherFetchPolicy(
        this.department,
        this._authService,
        this._appConfig,
      ),
    );
    this.fetchData();
  }

  protected fetchData(): void {
    this._dataService.fetch().subscribe((response) => {
      this.teachers = response;
    });
  }
}
