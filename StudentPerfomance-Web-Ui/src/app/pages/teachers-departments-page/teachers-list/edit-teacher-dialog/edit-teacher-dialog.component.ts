import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Department } from '../../../../modules/administration/submodules/departments/models/departments.interface';
import { Teacher } from '../../../../modules/administration/submodules/teachers/models/teacher.interface';
import { EditTeacherService } from './edit-teacher.service';
import { NotificationService } from '../../../../building-blocks/notifications/notification.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { FloatingLabelInputComponent } from '../../../../building-blocks/floating-label-input/floating-label-input.component';
import { GreenOutlineButtonComponent } from '../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { NgIf } from '@angular/common';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { SelectJobTitleDropdownComponent } from '../create-teacher-dialog/select-job-title-dropdown/select-job-title-dropdown.component';
import { SelectStateDropdownComponent } from '../create-teacher-dialog/select-state-dropdown/select-state-dropdown.component';
import { RedButtonComponent } from '../../../../building-blocks/buttons/red-button/red-button.component';

@Component({
  selector: 'app-edit-teacher-dialog',
  imports: [
    FloatingLabelInputComponent,
    GreenOutlineButtonComponent,
    NgIf,
    RedOutlineButtonComponent,
    SelectJobTitleDropdownComponent,
    SelectStateDropdownComponent,
    RedButtonComponent,
  ],
  templateUrl: './edit-teacher-dialog.component.html',
  styleUrl: './edit-teacher-dialog.component.scss',
  standalone: true,
})
export class EditTeacherDialogComponent implements OnInit {
  @Input({ required: true }) department: Department;
  @Input({ required: true }) teacher: Teacher;
  @Output() visibilityChanged: EventEmitter<void> = new EventEmitter();
  public teacherCopy: Teacher;
  public isSelectingJobTitle: boolean = false;
  public isSelectingState: boolean = false;

  public constructor(
    private readonly _service: EditTeacherService,
    private readonly _notifications: NotificationService,
  ) {}

  public ngOnInit() {
    this.teacherCopy = { ...this.teacher };
  }

  public edit(): void {
    this._service
      .edit(this.teacher, this.teacherCopy, this.department)
      .pipe(
        tap(() => {
          this.teacher.name = this.teacherCopy.name;
          this.teacher.surname = this.teacherCopy.surname;
          this.teacher.patronymic = this.teacherCopy.patronymic;
          this.teacher.jobTitle = this.teacherCopy.jobTitle;
          this.teacher.state = this.teacherCopy.state;
          this._notifications.setMessage('Изменены данные о преподавателе');
          this._notifications.success();
          this._notifications.turn();
          this.visibilityChanged.emit();
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

  public reset(): void {
    this.ngOnInit();
  }
}
