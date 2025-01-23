import { Component, EventEmitter, Input, Output } from '@angular/core';
import { RedButtonComponent } from '../../../../building-blocks/buttons/red-button/red-button.component';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { Department } from '../../../../modules/administration/submodules/departments/models/departments.interface';
import { Teacher } from '../../../../modules/administration/submodules/teachers/models/teacher.interface';
import { RemoveTeacherService } from './remove-teacher.service';
import { NotificationService } from '../../../../building-blocks/notifications/notification.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-remove-teacher-dialog',
  imports: [RedButtonComponent, RedOutlineButtonComponent],
  templateUrl: './remove-teacher-dialog.component.html',
  styleUrl: './remove-teacher-dialog.component.scss',
  standalone: true,
})
export class RemoveTeacherDialogComponent {
  @Input({ required: true }) department: Department;
  @Input({ required: true }) teacher: Teacher;
  @Output() visibilityChanged: EventEmitter<void> = new EventEmitter();
  @Output() teacherRemoved: EventEmitter<Teacher> = new EventEmitter();

  public constructor(
    private readonly _service: RemoveTeacherService,
    private readonly _notifications: NotificationService,
  ) {}

  public remove(): void {
    this._service
      .remove(this.teacher, this.department)
      .pipe(
        tap(() => {
          this._notifications.setMessage('Преподаватель был удален');
          this._notifications.success();
          this._notifications.turn();
          this.teacherRemoved.emit(this.teacher);
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
}
