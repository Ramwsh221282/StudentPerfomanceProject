import { Component, OnInit } from '@angular/core';
import { BaseTeacherForm } from '../models/base-teacher-form';
import { ReactiveFormsModule } from '@angular/forms';
import { NgIf } from '@angular/common';
import { FacadeTeacherService } from '../services/facade-teacher.service';
import { UserOperationNotificationService } from '../../../../shared-services/user-notifications/user-operation-notification-service.service';
import { INotificatable } from '../../../../shared-models/interfaces/inotificated-component-interface/inotificatable.interface';
import { TeacherDeletedHandler } from './teacher-deleted-handler';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { TeacherUpdatedHandler } from './teacher-updated-handler';
import { SuccessNotificationFormComponent } from '../../../../notification-modal-forms/success-notification-form/success-notification-form.component';
import { FailureNotificationFormComponent } from '../../../../notification-modal-forms/failure-notification-form/failure-notification-form.component';
import { ModalState } from '../../../../shared-models/models/modals/modal-state';

@Component({
  selector: 'app-manage-teacher-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    NgIf,
    SuccessNotificationFormComponent,
    FailureNotificationFormComponent,
  ],
  templateUrl: './manage-teacher-form.component.html',
  styleUrl: './manage-teacher-form.component.scss',
})
export class ManageTeacherFormComponent
  extends BaseTeacherForm
  implements OnInit, INotificatable
{
  public readonly successModalState: ModalState = new ModalState();
  public readonly failureModalState: ModalState = new ModalState();
  public constructor(
    protected readonly facadeService: FacadeTeacherService,
    private readonly _notificationService: UserOperationNotificationService
  ) {
    super('Управление преподавателями');
  }

  public ngOnInit(): void {
    this.initForm();
  }

  protected override submit(): void {
    this.ngOnInit();
  }

  protected deleteClick(): void {
    const handler = TeacherDeletedHandler(
      this.facadeService,
      this,
      this._notificationService
    );
    this.facadeService
      .delete(this.createTeacherFromForm(this.facadeService.currentDepartment))
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
    this.submit();
  }

  protected editClick(): void {
    const handler = TeacherUpdatedHandler(
      this.facadeService,
      this,
      this._notificationService
    );
    this.facadeService
      .update(this.createTeacherFromForm(this.facadeService.currentDepartment))
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
    this.submit();
  }
}
