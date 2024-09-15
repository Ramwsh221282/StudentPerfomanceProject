import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BaseStudentGroupForm } from '../../models/base-student-group-form';
import { NgIf } from '@angular/common';
import { SuccessNotificationFormComponent } from '../../../../notification-modal-forms/success-notification-form/success-notification-form.component';
import { HttpErrorResponse } from '@angular/common/http';
import { UserOperationNotificationService } from '../../../../shared-services/user-notifications/user-operation-notification-service.service';
import { catchError, tap } from 'rxjs';
import { INotificatable } from '../../../../shared-models/interfaces/inotificated-component-interface/inotificatable.interface';
import { FailureNotificationFormComponent } from '../../../../notification-modal-forms/failure-notification-form/failure-notification-form.component';
import { StudentGroupsFacadeService } from '../../services/student-groups-facade.service';
import { StudentGroupCreationHandler } from './studentgroup-creation-handler';
import { ModalState } from '../../../../shared-models/models/modals/modal-state';

@Component({
  selector: 'app-dbmanager-studentgroups-create-group-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    NgIf,
    SuccessNotificationFormComponent,
    FailureNotificationFormComponent,
  ],
  templateUrl: './dbmanager-studentgroups-create-group-form.component.html',
  styleUrl: './dbmanager-studentgroups-create-group-form.component.scss',
  providers: [UserOperationNotificationService],
})
export class DbmanagerStudentgroupsCreateGroupFormComponent
  extends BaseStudentGroupForm
  implements INotificatable, OnInit
{
  public readonly successModalState: ModalState = new ModalState();
  public readonly failureModalState: ModalState = new ModalState();
  public constructor(
    private readonly _facadeService: StudentGroupsFacadeService,
    private readonly _notificationService: UserOperationNotificationService
  ) {
    super('Добавление студента');
  }

  public ngOnInit(): void {
    this.initForm();
  }

  protected submit(): void {
    const handler = StudentGroupCreationHandler(
      this._facadeService,
      this,
      this._notificationService
    );
    this._facadeService
      .create(this.createStudentGroupFromForm())
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
    this.ngOnInit();
  }
}
