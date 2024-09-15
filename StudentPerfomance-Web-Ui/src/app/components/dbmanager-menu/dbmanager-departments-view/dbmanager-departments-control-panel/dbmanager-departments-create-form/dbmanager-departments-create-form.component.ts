import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { DepartmentFormBase } from '../../models/base-department-form';
import { INotificatable } from '../../../../shared-models/interfaces/inotificated-component-interface/inotificatable.interface';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { UserOperationNotificationService } from '../../../../shared-services/user-notifications/user-operation-notification-service.service';
import { SuccessNotificationFormComponent } from '../../../../notification-modal-forms/success-notification-form/success-notification-form.component';
import { FailureNotificationFormComponent } from '../../../../notification-modal-forms/failure-notification-form/failure-notification-form.component';
import { NgIf } from '@angular/common';
import { DepartmentsFacadeService } from '../../services/departments-facade.service';
import { DepartmentCreationHandler } from './department-creation-handler';
import { ModalState } from '../../../../shared-models/models/modals/modal-state';

@Component({
  selector: 'app-dbmanager-departments-create-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    SuccessNotificationFormComponent,
    FailureNotificationFormComponent,
    NgIf,
  ],
  templateUrl: './dbmanager-departments-create-form.component.html',
  styleUrl: './dbmanager-departments-create-form.component.scss',
})
export class DbmanagerDepartmentsCreateFormComponent
  extends DepartmentFormBase
  implements OnInit, INotificatable
{
  public readonly successModalState: ModalState = new ModalState();
  public readonly failureModalState: ModalState = new ModalState();
  public constructor(
    private readonly _facadeService: DepartmentsFacadeService,
    private readonly _notificationService: UserOperationNotificationService
  ) {
    super('Добавление кафедры');
  }

  public ngOnInit(): void {
    this.initForm();
  }

  protected override submit(): void {
    const handler = DepartmentCreationHandler(
      this._facadeService,
      this,
      this._notificationService
    );
    this._facadeService
      .create(this.createDepartmentFromForm())
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
    this.ngOnInit();
  }
}
