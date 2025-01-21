import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Teacher } from '../../../../../teachers/models/teacher.interface';
import { UserOperationNotificationService } from '../../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { TeacherEditService } from '../../../../services/teacher-edit.service';
import { IsNullOrWhiteSpace } from '../../../../../../../../shared/utils/string-helper';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { Department } from '../../../../models/departments.interface';

@Component({
    selector: 'app-teacher-edit-dropdown',
    templateUrl: './teacher-edit-dropdown.component.html',
    styleUrl: './teacher-edit-dropdown.component.scss',
    standalone: false
})
export class TeacherEditDropdownComponent implements OnInit {
  @Input({ required: true }) department: Department;
  @Input({ required: true }) teacher: Teacher;
  @Input({ required: true }) visibility: boolean = false;
  @Output() visibilityChanged: EventEmitter<boolean> = new EventEmitter();
  protected teacherName: string = '';
  protected teacherSurname: string = '';
  protected teacherPatronymic: string = '';
  protected teacherState: string = '';
  protected teacherJobTitle: string = '';
  protected isSelectingStates: boolean = false;
  protected isSelectingJobTitles: boolean = false;
  protected readonly jobTitles: string[] = [
    'Ассистент',
    'Доцент',
    'Профессор',
    'Старший преподаватель',
  ];
  protected readonly states: string[] = ['Штатный', 'Внешний совместитель'];

  public constructor(
    private readonly _notificationService: UserOperationNotificationService,
    private readonly _editService: TeacherEditService,
  ) {}

  public ngOnInit(): void {
    this.teacherName = this.teacher.name;
    this.teacherSurname = this.teacher.surname;
    this.teacherPatronymic =
      this.teacher.patronymic == null ? '' : this.teacher.patronymic;
    this.teacherState = this.teacher.state;
    this.teacherJobTitle = this.teacher.jobTitle;
    this.teacher.department = { ...this.department };
  }

  public submit(): void {
    if (this.isNameEmpty()) return;
    if (this.isSurnameEmpty()) return;
    if (this.isStateEmpty()) return;
    if (this.isJobTitleEmpty()) return;
    const updatedTeacher = this.createUpdatedTeacher();
    const notification = this.createTeacherUpdateMessage(
      this.teacher,
      updatedTeacher,
    );
    this._editService
      .update(this.teacher, updatedTeacher)
      .pipe(
        tap((response) => {
          this.updateTeacher(updatedTeacher);
          this._notificationService.SetMessage = notification;
          this._notificationService.success();
          this.close();
        }),
        catchError((error: HttpErrorResponse) => {
          this._notificationService.SetMessage = error.error;
          this._notificationService.failure();
          this.close();
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  protected close(): void {
    this.visibility = false;
    this.visibilityChanged.emit(this.visibility);
  }

  protected handleJobTitleSelect(jobTitle: string): void {
    this.teacherJobTitle = jobTitle;
  }

  protected handleStateSelect(state: string): void {
    this.teacherState = state;
  }

  protected isNameEmpty(): boolean {
    if (IsNullOrWhiteSpace(this.teacherName)) {
      this._notificationService.SetMessage = 'Имя было пустым';
      this._notificationService.failure();
      return true;
    }
    return false;
  }

  protected isSurnameEmpty(): boolean {
    if (IsNullOrWhiteSpace(this.teacherSurname)) {
      this._notificationService.SetMessage = 'Фамилия была пустой';
      this._notificationService.failure();
      return true;
    }
    return false;
  }

  protected isStateEmpty(): boolean {
    if (IsNullOrWhiteSpace(this.teacherState)) {
      this._notificationService.SetMessage = 'Условие работы было пустым';
      this._notificationService.failure();
      return true;
    }
    return false;
  }

  protected isJobTitleEmpty(): boolean {
    if (IsNullOrWhiteSpace(this.teacherJobTitle)) {
      this._notificationService.SetMessage = 'Должность была пустой';
      this._notificationService.failure();
      return true;
    }
    return false;
  }

  protected createUpdatedTeacher(): Teacher {
    const updatedTeacher = { ...this.teacher };
    updatedTeacher.name = this.teacherName;
    updatedTeacher.surname = this.teacherSurname;
    updatedTeacher.patronymic = this.teacherPatronymic;
    updatedTeacher.state = this.teacherState;
    updatedTeacher.jobTitle = this.teacherJobTitle;
    return updatedTeacher;
  }

  protected updateTeacher(updatedTeacher: Teacher): void {
    this.teacher.name = updatedTeacher.name;
    this.teacher.surname = updatedTeacher.surname;
    this.teacher.patronymic = updatedTeacher.patronymic;
    this.teacher.state = updatedTeacher.state;
    this.teacher.jobTitle = updatedTeacher.jobTitle;
  }

  protected createTeacherUpdateMessage(
    oldTeacherData: Teacher,
    newTeacherData: Teacher,
  ): string {
    const notificationArray: string[] = [];
    notificationArray.push(`Изменены данные о преподавателе`);
    notificationArray.push(`Прошлая информация:`);
    notificationArray.push(
      `${oldTeacherData.surname} ${oldTeacherData.name} ${oldTeacherData.patronymic} ${oldTeacherData.state} ${oldTeacherData.jobTitle}`,
    );
    notificationArray.push(`Новая информация:`);
    notificationArray.push(
      `${newTeacherData.surname} ${newTeacherData.name} ${newTeacherData.patronymic} ${newTeacherData.state} ${newTeacherData.jobTitle}`,
    );
    return notificationArray.join('\n');
  }
}
