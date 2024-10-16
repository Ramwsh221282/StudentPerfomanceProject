import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ISubbmittable } from '../../../../../../../../shared/models/interfaces/isubbmitable';
import { Teacher } from '../../../../../teachers/models/teacher.interface';
import { TeacherEditService } from './teacher-edit.service';
import { UserOperationNotificationService } from '../../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { TeacherEditHandler } from './teacher-edit-handler';
import { BaseTeacherForm } from '../../../../../teachers/models/base-teacher-form';
import { catchError, tap } from 'rxjs';

@Component({
  selector: 'app-teacher-edit-modal',
  templateUrl: './teacher-edit-modal.component.html',
  styleUrl: './teacher-edit-modal.component.scss',
  providers: [TeacherEditService],
})
export class TeacherEditModalComponent
  extends BaseTeacherForm
  implements ISubbmittable, OnInit
{
  @Input({ required: true }) initial: Teacher;
  @Output() visibilityEmitter: EventEmitter<boolean> = new EventEmitter();
  @Output() successEmitter: EventEmitter<void> = new EventEmitter();
  @Output() failureEmitter: EventEmitter<void> = new EventEmitter();
  @Output() refreshEmitter: EventEmitter<void> = new EventEmitter();

  protected activeTeacher: Teacher;

  public constructor(
    private readonly _notificationService: UserOperationNotificationService,
    private readonly _editService: TeacherEditService
  ) {
    super();
    this.activeTeacher = {} as Teacher;
  }

  public ngOnInit(): void {
    this.initForm();
    this.activeTeacher = { ...this.initial };
  }

  public submit(): void {
    const updated = this.createTeacherFromForm(this.initial.department);
    const handler = TeacherEditHandler(
      this._notificationService,
      this.successEmitter,
      this.failureEmitter,
      this.initial
    );

    this._editService
      .update(this.initial, updated)
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
