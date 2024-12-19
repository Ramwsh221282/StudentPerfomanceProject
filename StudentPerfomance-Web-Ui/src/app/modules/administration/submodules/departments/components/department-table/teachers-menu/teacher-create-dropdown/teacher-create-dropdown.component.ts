import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ISubbmittable } from '../../../../../../../../shared/models/interfaces/isubbmitable';
import { Teacher } from '../../../../../teachers/models/teacher.interface';
import { Department } from '../../../../models/departments.interface';
import { UserOperationNotificationService } from '../../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { TeacherCreationService } from '../../../../services/teacher-creation.service';
import { IsNullOrWhiteSpace } from '../../../../../../../../shared/utils/string-helper';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-teacher-create-dropdown',
  templateUrl: './teacher-create-dropdown.component.html',
  styleUrl: './teacher-create-dropdown.component.scss',
})
export class TeacherCreateDropdownComponent implements ISubbmittable {
  @Input({ required: true }) department: Department;
  @Input({ required: true }) visibility: boolean = false;
  @Output() visibilityChange: EventEmitter<boolean> = new EventEmitter();
  @Output() teacherCreated: EventEmitter<Teacher> = new EventEmitter();

  protected readonly jobTitles: string[] = [
    'Ассистент',
    'Доцент',
    'Профессор',
    'Старший преподаватель',
  ];
  protected readonly states: string[] = ['Штатный', 'Внешний совместитель'];

  protected teacherName: string = '';
  protected teacherSurname: string = '';
  protected teacherPatronymic: string = '';
  protected teacherState: string = '';
  protected teacherJobTitle: string = '';

  protected isSelectingState: boolean = false;
  protected isSelectingJobTitle: boolean = false;

  public constructor(
    private readonly _notificationService: UserOperationNotificationService,
    private readonly _createService: TeacherCreationService,
  ) {}

  public submit(): void {
    if (this.isNameEmpty()) return;
    if (this.isSurnameEmpty()) return;
    if (this.isStateEmpty()) return;
    if (this.isJobTitleEmpty()) return;
    const teacher = this.createNewTeacher();
    const message = this.createTeacherAddedNotification(teacher);
    this._createService
      .create(teacher)
      .pipe(
        tap((response) => {
          this._notificationService.SetMessage = message;
          this._notificationService.success();
          this.teacherCreated.emit(teacher);
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

  private cleanInputs(): void {
    this.teacherName = '';
    this.teacherSurname = '';
    this.teacherPatronymic = '';
    this.teacherState = '';
    this.teacherJobTitle = '';
  }

  private isNameEmpty(): boolean {
    if (IsNullOrWhiteSpace(this.teacherName)) {
      this._notificationService.SetMessage = 'Не указано имя';
      this._notificationService.failure();
      return true;
    }
    return false;
  }

  private isSurnameEmpty(): boolean {
    if (IsNullOrWhiteSpace(this.teacherSurname)) {
      this._notificationService.SetMessage = 'Не указана фамилия';
      this._notificationService.failure();
      return true;
    }
    return false;
  }

  private isStateEmpty(): boolean {
    if (IsNullOrWhiteSpace(this.teacherState)) {
      this._notificationService.SetMessage = 'Не указано условие работы';
      this._notificationService.failure();
      return true;
    }
    return false;
  }

  private isJobTitleEmpty(): boolean {
    if (IsNullOrWhiteSpace(this.teacherJobTitle)) {
      this._notificationService.SetMessage = 'Не указана должность';
      this._notificationService.failure();
      return true;
    }
    return false;
  }

  private createNewTeacher(): Teacher {
    const teacher = {} as Teacher;
    teacher.name = this.teacherName;
    teacher.surname = this.teacherSurname;
    teacher.patronymic = this.teacherPatronymic;
    teacher.state = this.teacherState;
    teacher.jobTitle = this.teacherJobTitle;
    teacher.department = { ...this.department };
    return teacher;
  }

  private createTeacherAddedNotification(teacher: Teacher): string {
    return `Добавлен преподаватель ${teacher.surname} ${teacher.name} ${teacher.patronymic} в кафедру ${teacher.department.name}`;
  }
}
