import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BaseStudentGroupForm } from '../../models/base-student-group-form';
import { INotificatable } from '../../../../shared-models/interfaces/inotificated-component-interface/inotificatable.interface';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { UserOperationNotificationService } from '../../../../shared-services/user-notifications/user-operation-notification-service.service';
import { SuccessNotificationFormComponent } from '../../../../notification-modal-forms/success-notification-form/success-notification-form.component';
import { FailureNotificationFormComponent } from '../../../../notification-modal-forms/failure-notification-form/failure-notification-form.component';
import { NgIf } from '@angular/common';
import { StudentGroupDeletionHandler } from './studentgroup-deletion-handler';
import { StudentGroupUpdateHandler } from './studentgroup-update-handler';
import { StudentGroupsFacadeService } from '../../services/student-groups-facade.service';
import { ModalState } from '../../../../shared-models/models/modals/modal-state';

@Component({
  selector: 'app-dbmanager-studentgroups-edit-group-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    SuccessNotificationFormComponent,
    FailureNotificationFormComponent,
    NgIf,
  ],
  templateUrl: './dbmanager-studentgroups-edit-group-form.component.html',
  styleUrl: './dbmanager-studentgroups-edit-group-form.component.scss',
})
export class DbmanagerStudentgroupsEditGroupFormComponent
  extends BaseStudentGroupForm
  implements INotificatable, OnInit
{
  public readonly successModalState: ModalState = new ModalState();
  public readonly failureModalState: ModalState = new ModalState();

  public constructor(
    protected readonly facadeService: StudentGroupsFacadeService,
    private readonly _notificationService: UserOperationNotificationService
  ) {
    super('Редактирование группы');
  }

  public ngOnInit(): void {
    this.initForm();
  }

  protected submit(): void {
    this.ngOnInit();
  }

  protected deleteOperationClick(): void {
    const handler = StudentGroupDeletionHandler(
      this.facadeService,
      this,
      this._notificationService
    );
    this.facadeService
      .delete()
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
    this.submit();
  }

  protected editOperationClick(): void {
    const handler = StudentGroupUpdateHandler(
      this.facadeService,
      this,
      this.facadeService.copy,
      this._notificationService
    );
    this.facadeService
      .update(this.createStudentGroupFromForm())
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
    this.submit();
  }
}
