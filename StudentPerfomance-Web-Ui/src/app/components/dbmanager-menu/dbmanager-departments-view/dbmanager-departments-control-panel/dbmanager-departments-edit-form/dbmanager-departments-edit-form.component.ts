import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { DepartmentFormBase } from '../../models/base-department-form';
import { DepartmentsFacadeService } from '../../services/departments-facade.service';
import { UserOperationNotificationService } from '../../../../shared-services/user-notifications/user-operation-notification-service.service';
import { DepartmentUpdateHandler } from './department-update-handler';
import { INotificatable } from '../../../../shared-models/interfaces/inotificated-component-interface/inotificatable.interface';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { DepartmentDeletionHandler } from './department-deletion-handler';
import { SuccessNotificationFormComponent } from '../../../../notification-modal-forms/success-notification-form/success-notification-form.component';
import { FailureNotificationFormComponent } from '../../../../notification-modal-forms/failure-notification-form/failure-notification-form.component';
import { NgIf } from '@angular/common';
import { ModalState } from '../../../../shared-models/models/modals/modal-state';

@Component({
  selector: 'app-dbmanager-departments-edit-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    SuccessNotificationFormComponent,
    FailureNotificationFormComponent,
    NgIf,
  ],
  templateUrl: './dbmanager-departments-edit-form.component.html',
  styleUrl: './dbmanager-departments-edit-form.component.scss',
})
export class DbmanagerDepartmentsEditFormComponent
  extends DepartmentFormBase
  implements OnInit, INotificatable
{
  public readonly successModalState: ModalState = new ModalState();
  public readonly failureModalState: ModalState = new ModalState();
  public constructor(
    protected readonly facadeService: DepartmentsFacadeService,
    private readonly _notificationService: UserOperationNotificationService
  ) {
    super('Редактирование кафедры');
  }

  public ngOnInit(): void {
    this.initForm();
  }

  protected override submit(): void {
    this.initForm();
  }

  protected deleteClick(): void {
    const handler = DepartmentDeletionHandler(
      this.facadeService,
      this,
      this._notificationService
    );
    this.facadeService
      .delete(this.createDepartmentFromForm())
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
    this.submit();
  }

  protected editClick(): void {
    const handler = DepartmentUpdateHandler(
      this.facadeService,
      this,
      this._notificationService
    );
    this.facadeService
      .update(this.createDepartmentFromForm())
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
    this.submit();
  }
}
