import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Department } from '../../../../modules/administration/submodules/departments/models/departments.interface';
import { Teacher } from '../../../../modules/administration/submodules/teachers/models/teacher.interface';
import { CreateTeacherService } from './create-teacher.service';
import { NotificationService } from '../../../../building-blocks/notifications/notification.service';
import { IsNullOrWhiteSpace } from '../../../../shared/utils/string-helper';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { FloatingLabelInputComponent } from '../../../../building-blocks/floating-label-input/floating-label-input.component';
import { GreenOutlineButtonComponent } from '../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { SelectJobTitleDropdownComponent } from './select-job-title-dropdown/select-job-title-dropdown.component';
import { NgIf } from '@angular/common';
import { SelectStateDropdownComponent } from './select-state-dropdown/select-state-dropdown.component';
import { animate, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-create-teacher-dialog',
  imports: [
    FloatingLabelInputComponent,
    GreenOutlineButtonComponent,
    RedOutlineButtonComponent,
    SelectJobTitleDropdownComponent,
    NgIf,
    SelectStateDropdownComponent,
  ],
  templateUrl: './create-teacher-dialog.component.html',
  styleUrl: './create-teacher-dialog.component.scss',
  standalone: true,
  animations: [
    trigger('fadeIn', [
      transition(':enter', [
        style({ opacity: 0, transform: 'translateY(-10px)' }),
        animate(
          '300ms ease-out',
          style({ opacity: 1, transform: 'translateY(0)' }),
        ),
      ]),
      transition(':leave', [
        animate(
          '300ms ease-in',
          style({ opacity: 0, transform: 'translateY(-10px)' }),
        ),
      ]),
    ]),
  ],
})
export class CreateTeacherDialogComponent {
  @Input({ required: true }) department: Department;
  @Output() teacherCreated: EventEmitter<Teacher> = new EventEmitter();
  @Output() visibilityChanged: EventEmitter<void> = new EventEmitter();
  public name: string = '';
  public surname: string = '';
  public patronymic: string = '';
  public jobTitle: string = '';
  public state: string = '';
  public isSelectingJobTitle: boolean = false;
  public isSelectingState: boolean = false;

  public constructor(
    private readonly _service: CreateTeacherService,
    private readonly _notifications: NotificationService,
  ) {}

  public create(): void {
    if (IsNullOrWhiteSpace(this.name)) {
      this._notifications.setMessage('Имя не должно быть пустым');
      this._notifications.failure();
      this._notifications.turn();
      return;
    }
    if (IsNullOrWhiteSpace(this.surname)) {
      this._notifications.setMessage('Фамилия не должна быть пустая');
      this._notifications.failure();
      this._notifications.turn();
    }
    if (IsNullOrWhiteSpace(this.jobTitle)) {
      this._notifications.setMessage('Должность должна быть указана');
      this._notifications.failure();
      this._notifications.turn();
    }
    if (IsNullOrWhiteSpace(this.state)) {
      this._notifications.setMessage('Способ работы должен быть указан');
      this._notifications.failure();
      this._notifications.turn();
    }
    const teacher: Teacher = {} as Teacher;
    teacher.name = this.name;
    teacher.surname = this.surname;
    teacher.patronymic = this.patronymic;
    teacher.jobTitle = this.jobTitle;
    teacher.state = this.state;
    this._service
      .create(teacher, this.department)
      .pipe(
        tap((response) => {
          this._notifications.setMessage('Преподаватель добавлен в кафедру');
          this._notifications.success();
          this._notifications.turn();
          teacher.id = response.id;
          this.teacherCreated.emit(teacher);
          this.updateInputs();
        }),
        catchError((error: HttpErrorResponse) => {
          this._notifications.setMessage(error.error);
          this._notifications.failure();
          this._notifications.turn();
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  private updateInputs(): void {
    this.name = '';
    this.surname = '';
    this.patronymic = '';
    this.jobTitle = '';
    this.state = '';
  }
}
