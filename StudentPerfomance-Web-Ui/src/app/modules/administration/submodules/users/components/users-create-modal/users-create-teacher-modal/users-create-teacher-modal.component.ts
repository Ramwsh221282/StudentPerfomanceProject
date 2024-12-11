import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { UserCreationService } from '../../../services/user-create.service';
import { DepartmentDataService } from '../../../../departments/components/department-table/department-data.service';
import { TeacherDataService } from '../../../../departments/components/department-table/department-teachers-menu-modal/teachers-data.service';
import { Department } from '../../../../departments/models/departments.interface';
import { Teacher } from '../../../../teachers/models/teacher.interface';
import { User } from '../../../../../../users/services/user-interface';
import { ISubbmittable } from '../../../../../../../shared/models/interfaces/isubbmitable';
import { DefaultTeacherFetchPolicy } from '../../../../teachers/models/fething-policies/default-teachers-fetch-policy';
import { AuthService } from '../../../../../../users/services/auth.service';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { UserCreationHandler } from '../users-create-handler';
import { catchError, tap } from 'rxjs';
import { AppConfigService } from '../../../../../../../app.config.service';

@Component({
  selector: 'app-users-create-teacher-modal',
  templateUrl: './users-create-teacher-modal.component.html',
  styleUrl: './users-create-teacher-modal.component.scss',
  providers: [UserCreationService, DepartmentDataService, TeacherDataService],
})
export class UsersCreateTeacherModalComponent implements OnInit, ISubbmittable {
  @Output() visibility: EventEmitter<boolean> = new EventEmitter();
  @Output() success: EventEmitter<void> = new EventEmitter();
  @Output() failure: EventEmitter<void> = new EventEmitter();
  @Output() refresh: EventEmitter<void> = new EventEmitter();

  protected departments: Department[];
  protected teachers: Teacher[];
  protected user: User;

  public constructor(
    private readonly _userCreationService: UserCreationService,
    private readonly _departmentsFetchService: DepartmentDataService,
    private readonly _teacherFetchService: TeacherDataService,
    private readonly _authService: AuthService,
    private readonly _notificationService: UserOperationNotificationService,
    private readonly _appConfig: AppConfigService,
  ) {
    this.departments = [];
    this.teachers = [];
    this.initUser();
  }

  private initUser(): void {
    this.user = {} as User;
    this.user.role = 'Преподаватель';
    this.user.email = '';
  }

  public ngOnInit(): void {
    this.fetchDeaprtments();
  }

  public submit(): void {
    const handler = UserCreationHandler(
      this._notificationService,
      this.success,
      this.failure,
      this.refresh,
      this.visibility,
    );
    this._userCreationService
      .create(this.user)
      .pipe(
        tap((response) => {
          handler.handle(response);
          this.refresh.emit();
          this.visibility.emit(false);
        }),
        catchError((error) => handler.handleError(error)),
      )
      .subscribe();
    this.initUser();
  }

  protected fetchDeaprtments(): void {
    this._departmentsFetchService.getAllDepartments().subscribe((response) => {
      this.departments = response;
      if (this.departments.length > 0)
        this.fetchTeachers(this.departments[0].name);
    });
  }

  protected fetchTeachers(departmentName: string): void {
    const department = {} as Department;
    department.name = departmentName;
    const policy = new DefaultTeacherFetchPolicy(
      department,
      this._authService,
      this._appConfig,
    );
    this._teacherFetchService.setPolicy(policy);
    this._teacherFetchService.fetch().subscribe((response) => {
      this.teachers = response;
      if (this.teachers.length > 0) {
        this.user.name = this.teachers[0].name;
        this.user.surname = this.teachers[0].surname;
        this.user.patronymic = this.teachers[0].patronymic;
      }
    });
  }

  protected selectDepartment(departmentName: any): void {
    const name: string = departmentName.target.value;
    this.fetchTeachers(name);
  }

  protected selectTeacher(data: any): void {
    const selectedTeacherNames = data.target.value;
    const parts = selectedTeacherNames.split(' ');
    const teacher = this.teachers.find(
      (t) =>
        t.surname == parts[0] && t.name == parts[1] && t.patronymic == parts[2],
    );
    this.user.name = teacher!.name;
    this.user.surname = teacher!.surname;
    this.user.patronymic = teacher!.patronymic;
  }
}
