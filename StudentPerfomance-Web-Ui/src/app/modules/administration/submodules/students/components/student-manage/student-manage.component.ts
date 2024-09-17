import { Component, Input, OnInit } from '@angular/core';
import { BaseStudentForm } from '../../models/base-student-form';
import { FacadeStudentService } from '../../services/facade-student.service';
import { StudentDeletionHandler } from './student-deletion-handler';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { StudentUpdateHandler } from './student-update-handler';
import { INotificatable } from '../../../../../../shared/models/inotificated-component-interface/inotificatable.interface';
import { ModalState } from '../../../../../../shared/models/modals/modal-state';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { StudentGroup } from '../../../student-groups/services/studentsGroup.interface';

@Component({
  selector: 'app-student-manage',
  templateUrl: './student-manage.component.html',
  styleUrl: './student-manage.component.scss',
})
export class StudentManageComponent
  extends BaseStudentForm
  implements OnInit, INotificatable
{
  @Input({ required: true }) public currentGroup: StudentGroup;
  public successModalState: ModalState = new ModalState();
  public failureModalState: ModalState = new ModalState();
  public constructor(
    protected readonly facadeService: FacadeStudentService,
    private readonly _notificationService: UserOperationNotificationService
  ) {
    super('Редактирование студента');
  }
  protected override submit(): void {
    this.ngOnInit();
  }
  public ngOnInit(): void {
    this.initForm();
  }

  protected editClick(): void {
    const handler = StudentUpdateHandler(
      this.facadeService.copy,
      this.facadeService,
      this,
      this._notificationService
    );
    this.facadeService
      .update(this.createStudentFromForm(this.currentGroup))
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
    this.submit();
  }

  protected deleteClick(): void {
    const handler = StudentDeletionHandler(
      this.facadeService,
      this,
      this._notificationService
    );
    this.facadeService
      .delete(this.createStudentFromForm(this.currentGroup))
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
    this.submit();
  }
}
