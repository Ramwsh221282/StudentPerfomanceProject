import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { User } from '../../../../../../users/services/user-interface';
import { ISubbmittable } from '../../../../../../../shared/models/interfaces/isubbmitable';
import { UserCreationService } from '../../../services/user-create.service';
import { DepartmentDataService } from '../../../../departments/components/department-table/department-data.service';
import { TeacherDataService } from '../../../../departments/services/teachers-data.service';
import { Department } from '../../../../departments/models/departments.interface';
import { Teacher } from '../../../../teachers/models/teacher.interface';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { IsNullOrWhiteSpace } from '../../../../../../../shared/utils/string-helper';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-create-teacher-user-dropdown',
  templateUrl: './create-teacher-user-dropdown.component.html',
  styleUrl: './create-teacher-user-dropdown.component.scss',
  providers: [UserCreationService, DepartmentDataService, TeacherDataService],
})
export class CreateTeacherUserDropdownComponent
  implements ISubbmittable, OnInit
{
  @Input({ required: true }) visibility: boolean = false;
  @Output() visibilityChange: EventEmitter<boolean> =
    new EventEmitter<boolean>();
  @Output() userCreated: EventEmitter<User> = new EventEmitter();

  public constructor(
    private readonly _departmentsService: DepartmentDataService,
    private readonly _notificationService: UserOperationNotificationService,
    private readonly _createService: UserCreationService,
  ) {}

  private readonly _role: string = 'Преподаватель';
  protected userEmail: string = '';

  protected departments: Department[] = [];
  protected departmentNames: string[] = [];
  protected selectedDepartment: Department | null;
  protected selectDepartmentLabel: string = 'Выберите кафедру';
  protected isSelectingDepartment: boolean = false;

  protected teachers: Teacher[] = [];
  protected teacherNames: string[] = [];
  protected selectedTeacher: Teacher | null;
  protected selectTeacherLabel: string = 'Выберите преподавателя';
  protected isSelectingTeacher: boolean = false;

  public ngOnInit(): void {
    this._departmentsService.getAllDepartments().subscribe((response) => {
      this.departments = response;
      this.departmentNames = this.departments.map(
        (department) => department.name,
      );
    });
  }

  public submit(): void {
    if (this.isDepartmentNotSelected()) return;
    if (this.isTeacherNotSelected()) return;
    if (this.isEmailEmpty()) return;
    const user = this.createUser();
    this._createService
      .create(user)
      .pipe(
        tap((response) => {
          this._notificationService.SetMessage = `Добавлен пользователь-преподаватель ${user.surname} ${user.name} ${user.patronymic}`;
          this._notificationService.success();
          this.userCreated.emit(user);
          this.cleanInputs();
        }),
        catchError((error: HttpErrorResponse) => {
          this._notificationService.SetMessage = error.error;
          this._notificationService.failure();
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  protected close(): void {
    this.visibility = false;
    this.visibilityChange.emit(this.visibility);
  }

  protected handleSelectedDepartment(departmentName: string): void {
    this.selectDepartmentLabel = departmentName;
    this.selectedDepartment = this.departments.find(
      (department) => department.name == departmentName,
    )!;
    this.teachers = this.selectedDepartment.teachers;
    this.initTeacherNames();
    console.log(this.selectedDepartment);
  }

  private initTeacherNames(): void {
    const array: string[] = [];
    for (const teacher of this.teachers) {
      const surname = teacher.surname;
      const name = teacher.name;
      const patronymic = teacher.patronymic;
      const jobTitle = teacher.jobTitle;
      const state = teacher.state;
      const data = `${surname} ${name} ${patronymic} ${jobTitle} ${state}`;
      array.push(data);
    }
    this.teacherNames = array;
  }

  protected handleSelectedTeacher(teacherData: string): void {
    this.selectTeacherLabel = teacherData;
    this.selectedTeacher = this.parseSelectedTeacherData(teacherData);
    console.log(this.selectedTeacher);
  }

  private parseSelectedTeacherData(teacherData: string): Teacher {
    return this.teachers.find(
      (teacher) =>
        teacherData.includes(teacher.surname) &&
        teacherData.includes(teacher.name) &&
        teacherData.includes(teacher.patronymic) &&
        teacherData.includes(teacher.jobTitle) &&
        teacherData.includes(teacher.state),
    )!;
  }

  private isDepartmentNotSelected(): boolean {
    if (!this.selectedDepartment) {
      this._notificationService.SetMessage = 'Кафедра не была выбрана';
      this._notificationService.failure();
      return true;
    }
    return false;
  }

  private isTeacherNotSelected(): boolean {
    if (!this.selectedTeacher) {
      this._notificationService.SetMessage = 'Преподаватель не был выбран';
      this._notificationService.failure();
      return true;
    }
    return false;
  }

  private isEmailEmpty(): boolean {
    if (IsNullOrWhiteSpace(this.userEmail)) {
      this._notificationService.SetMessage = 'Почта была пустой';
      this._notificationService.failure();
      return true;
    }
    return false;
  }

  private createUser(): User {
    const user = {} as User;
    user.name = this.selectedTeacher!.name;
    user.surname = this.selectedTeacher!.surname;
    user.patronymic = this.selectedTeacher!.patronymic;
    user.email = this.userEmail;
    user.role = this._role;
    return user;
  }

  private cleanInputs(): void {
    this.userEmail = '';
    this.selectTeacherLabel = 'Выберите преподавателя';
    this.selectDepartmentLabel = 'Выберите кафедру';
    this.selectedDepartment = null;
    this.selectedTeacher = null;
  }
}
