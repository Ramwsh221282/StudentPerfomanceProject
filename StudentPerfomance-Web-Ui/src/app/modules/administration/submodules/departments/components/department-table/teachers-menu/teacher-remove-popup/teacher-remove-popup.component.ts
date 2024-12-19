import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Teacher } from '../../../../../teachers/models/teacher.interface';
import { UserOperationNotificationService } from '../../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { TeacherRemoveService } from '../../../../services/teacher-remove.service';
import { Department } from '../../../../models/departments.interface';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-teacher-remove-popup',
  templateUrl: './teacher-remove-popup.component.html',
  styleUrl: './teacher-remove-popup.component.scss',
})
export class TeacherRemovePopupComponent {
  @Input({ required: true }) department: Department;
  @Input({ required: true }) teacher: Teacher;
  @Input({ required: true }) visibility: boolean = false;
  @Output() visibilityChange: EventEmitter<boolean> = new EventEmitter();
  @Output() deletionCommit: EventEmitter<Teacher> = new EventEmitter<Teacher>();

  public constructor(
    private readonly _notificationService: UserOperationNotificationService,
    private readonly _teacherRemoveService: TeacherRemoveService,
  ) {}

  public submit(): void {
    this.teacher.department = { ...this.department };
    const message = this.buildNotificationMessage(this.teacher);
    this._teacherRemoveService
      .remove(this.teacher)
      .pipe(
        tap((response) => {
          this._notificationService.SetMessage = message;
          this._notificationService.success();
          this.deletionCommit.emit(this.teacher);
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

  public close(): void {
    this.visibility = false;
    this.visibilityChange.emit(this.visibility);
  }

  private buildNotificationMessage(teacher: Teacher): string {
    return `Удалён ${teacher.surname} ${teacher.name} ${teacher.patronymic} из кафедры ${this.department.name}`;
  }
}
