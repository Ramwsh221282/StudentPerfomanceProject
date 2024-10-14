import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ISubbmittable } from '../../../../../../../../shared/models/interfaces/isubbmitable';
import { TeacherRemoveService } from './teacher-remove.service';
import { UserOperationNotificationService } from '../../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { Teacher } from '../../../../../teachers/models/teacher.interface';
import { TeacherRemoveHandler } from './teacher-remove-handler';
import { catchError, tap } from 'rxjs';

@Component({
  selector: 'app-teacher-remove-modal',
  templateUrl: './teacher-remove-modal.component.html',
  styleUrl: './teacher-remove-modal.component.scss',
  providers: [TeacherRemoveService],
})
export class TeacherRemoveModalComponent implements ISubbmittable {
  @Input({ required: true }) teacher: Teacher;
  @Output() visibilityEmitter: EventEmitter<boolean> =
    new EventEmitter<boolean>();
  @Output() refreshEmitter: EventEmitter<void> = new EventEmitter<void>();
  @Output() successEmitter: EventEmitter<void> = new EventEmitter<void>();
  @Output() failureEmitter: EventEmitter<void> = new EventEmitter<void>();

  public constructor(
    private readonly _notificationService: UserOperationNotificationService,
    private readonly _removeService: TeacherRemoveService
  ) {}

  public submit(): void {
    const teacherToRemove = { ...this.teacher };
    const handler = TeacherRemoveHandler(
      this._notificationService,
      this.successEmitter,
      this.failureEmitter
    );
    this._removeService
      .remove(teacherToRemove)
      .pipe(
        tap((response) => {
          this.refreshEmitter.emit();
          this.visibilityEmitter.emit(false);
          handler.handle(response);
        }),
        catchError((error) => {
          this.visibilityEmitter.emit(false);
          return handler.handleError(error);
        })
      )
      .subscribe();
  }
}
