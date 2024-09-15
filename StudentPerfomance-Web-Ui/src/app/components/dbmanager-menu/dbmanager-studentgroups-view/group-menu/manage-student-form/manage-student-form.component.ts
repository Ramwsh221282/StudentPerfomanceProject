import { Component, OnInit } from '@angular/core';
import { BaseStudentForm } from '../models/base-student-form';
import { ReactiveFormsModule } from '@angular/forms';
import { INotificatable } from '../../../../shared-models/interfaces/inotificated-component-interface/inotificatable.interface';
import { catchError, tap } from 'rxjs';
import { UserOperationNotificationService } from '../../../../shared-services/user-notifications/user-operation-notification-service.service';
import { NgIf } from '@angular/common';
import { SuccessNotificationFormComponent } from '../../../../notification-modal-forms/success-notification-form/success-notification-form.component';
import { FailureNotificationFormComponent } from '../../../../notification-modal-forms/failure-notification-form/failure-notification-form.component';
import { HttpErrorResponse } from '@angular/common/http';
import { StudentDeletionHandler } from './student-deletion-handler';
import { StudentUpdateHandler } from './student-update-handler';
import { FacadeStudentService } from '../services/facade-student.service';
import { ModalState } from '../../../../shared-models/models/modals/modal-state';

@Component({
  selector: 'app-manage-student-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    NgIf,
    SuccessNotificationFormComponent,
    FailureNotificationFormComponent,
  ],
  templateUrl: './manage-student-form.component.html',
  styleUrl: './manage-student-form.component.scss',
})
export class ManageStudentFormComponent
  extends BaseStudentForm
  implements INotificatable, OnInit
{
  public readonly successModalState: ModalState = new ModalState();
  public readonly failureModalState: ModalState = new ModalState();
  public constructor(
    protected readonly facadeService: FacadeStudentService,
    private readonly _notificationService: UserOperationNotificationService
  ) {
    super('Редактирование студента');
  }

  public ngOnInit(): void {
    this.initForm();
  }

  protected override submit(): void {
    this.initForm();
  }

  protected deleteClick(): void {
    const handler = StudentDeletionHandler(
      this.facadeService,
      this,
      this._notificationService
    );
    this.facadeService
      .delete(this.createStudentFromForm(this.facadeService.currentGroup))
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
    this.submit();
  }

  protected updateClick(): void {
    const handler = StudentUpdateHandler(
      this.facadeService.copy,
      this.facadeService,
      this,
      this._notificationService
    );
    this.facadeService
      .update(this.createStudentFromForm(this.facadeService.currentGroup))
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
    this.submit();
  }
}
