import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { SemesterPlan } from '../../../../../../semester-plans/models/semester-plan.interface';
import { Department } from '../../../../../../departments/models/departments.interface';
import { Teacher } from '../../../../../../teachers/models/teacher.interface';
import { DepartmentBuilder } from '../../../../../../departments/models/builders/department-builder';
import { DepartmentDataService } from '../../../../../../departments/components/department-table/department-data.service';
import { TeacherDataService } from '../../../../../../departments/components/department-table/department-teachers-menu-modal/teachers-data.service';
import { DefaultTeacherFetchPolicy } from '../../../../../../teachers/models/fething-policies/default-teachers-fetch-policy';
import { TeacherBuilder } from '../../../../../../teachers/models/teacher-builder';
import { SemesterDisciplinesEditService } from './semester-disciplines-edit.service';
import { ISubbmittable } from '../../../../../../../../../shared/models/interfaces/isubbmitable';
import { catchError, Observable, tap } from 'rxjs';
import { UserOperationNotificationService } from '../../../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { AuthService } from '../../../../../../../../users/services/auth.service';
import { AppConfigService } from '../../../../../../../../../app.config.service';

@Component({
  selector: 'app-semester-disciplines-edit-modal',
  templateUrl: './semester-disciplines-edit-modal.component.html',
  styleUrl: './semester-disciplines-edit-modal.component.scss',
  providers: [
    DepartmentDataService,
    TeacherDataService,
    SemesterDisciplinesEditService,
  ],
})
export class SemesterDisciplinesEditModalComponent
  implements OnInit, ISubbmittable
{
  @Input({ required: true }) plan: SemesterPlan;
  @Output() visibilityEmitter: EventEmitter<boolean> = new EventEmitter();
  @Output() refreshEmitter: EventEmitter<any> = new EventEmitter();
  @Output() successEmitter: EventEmitter<void> = new EventEmitter();
  @Output() failureEmitter: EventEmitter<void> = new EventEmitter();

  protected departments: Department[];
  protected selectedDepartment: Department;
  protected deparmtentTeachers: Teacher[];
  protected selectedTeacher: Teacher;
  protected activePlan: SemesterPlan;

  public constructor(
    private readonly _departmentDataService: DepartmentDataService,
    private readonly _teacherDataService: TeacherDataService,
    private readonly _editService: SemesterDisciplinesEditService,
    private readonly _notificationService: UserOperationNotificationService,
    private readonly _authService: AuthService,
    private readonly _appConfig: AppConfigService,
  ) {
    this.activePlan = {} as SemesterPlan;
    this.departments = [];
    this.selectedDepartment = {} as Department;
    this.deparmtentTeachers = [];
    this.selectedTeacher = {} as Teacher;
  }

  public submit(): void {
    if (this.plan.discipline != this.activePlan.discipline) {
      this._editService
        .changeName(this.activePlan, this.plan)
        .pipe(
          tap((response) => {
            this.plan = { ...response };
            this._notificationService.SetMessage = 'Изменены данные дисциплины';
            this.successEmitter.emit();
            this.refreshEmitter.emit();
          }),
          catchError((error) => {
            this._notificationService.SetMessage = error.error;
            this.failureEmitter.emit();
            this.plan = { ...this.activePlan };
            return new Observable();
          }),
        )
        .subscribe();
    }
  }

  public ngOnInit(): void {
    this.activePlan = { ...this.plan };
    const builder = new DepartmentBuilder();
    this.selectedDepartment = { ...builder.buildDefault() };
    this.fetchDepartments();
  }

  protected fetchDepartments(): void {
    if (this.plan.teacher == null) {
      this._departmentDataService.getAllDepartments().subscribe((response) => {
        this.departments = response;
        if (this.departments.length > 0) {
          this.selectedDepartment = response[0];
          this.fetchDepartmentTeacher();
        }
      });
    }
  }

  protected selectDepartment(value: any): void {
    this.selectedDepartment = this.departments.find(
      (d) => d.name == value.target.value,
    )!;
    this.fetchDepartmentTeacher();
  }

  protected selectTeacher(teacher: Teacher): void {
    if (this.plan.teacher == null) {
      this.plan.teacher = { ...teacher };
      this._editService
        .attachTeacher(this.plan)
        .pipe(
          tap((response) => {
            this.plan = { ...response };
            this._notificationService.SetMessage = `Назначен преподаватель ${response.teacher!.surname} ${response.teacher!.name} ${response.teacher!.patronymic}`;
            this.successEmitter.emit();
            this.refreshEmitter.emit();
          }),
          catchError((error) => {
            this._notificationService.SetMessage = error.error;
            this.plan = { ...this.activePlan };
            this.failureEmitter.emit();
            return new Observable();
          }),
        )
        .subscribe();
    }
  }

  protected deattachTeacher(): void {
    if (this.plan.teacher != null) {
      const builder: TeacherBuilder = new TeacherBuilder();
      this.plan.teacher = { ...builder.buildDefault() };
      this._editService
        .deattachTeacher(this.plan)
        .pipe(
          tap((response) => {
            this._notificationService.SetMessage = `Преподаватель ${this.plan.teacher!.surname} ${this.plan.teacher!.name} ${this.plan.teacher!.patronymic} откреплен от дисциплины ${this.plan.discipline}`;
            this.successEmitter.emit();
            this.refreshEmitter.emit();
            this.plan = { ...response };
          }),
          catchError((error) => {
            this._notificationService.SetMessage = error.error;
            this.failureEmitter.emit();
            this.plan = { ...this.activePlan };
            return new Observable();
          }),
        )
        .subscribe();
      this.fetchDepartments();
    }
  }

  protected fetchDepartmentTeacher(): void {
    if (this.plan.teacher == null) {
      this._teacherDataService.setPolicy(
        new DefaultTeacherFetchPolicy(
          this.selectedDepartment,
          this._authService,
          this._appConfig,
        ),
      );
      this._teacherDataService.fetch().subscribe((response) => {
        this.deparmtentTeachers = response;
      });
    }
  }
}
