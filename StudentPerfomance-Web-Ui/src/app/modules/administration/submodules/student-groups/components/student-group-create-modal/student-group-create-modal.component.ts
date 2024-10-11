import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ISubbmittable } from '../../../../../../shared/models/interfaces/isubbmitable';
import { ISuccessNotificatable } from '../../../../../../shared/models/interfaces/isuccess-notificatable';
import { IFailureNotificatable } from '../../../../../../shared/models/interfaces/ifailure-notificatable';
import { BaseStudentGroupForm } from '../../models/base-student-group-form';
import { StudentGroupsFacadeService } from '../../services/student-groups-facade.service';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { StudentGroupCreationHandler } from './student-group-creation-handler';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-student-group-create-modal',
  templateUrl: './student-group-create-modal.component.html',
  styleUrl: './student-group-create-modal.component.scss',
})
export class StudentGroupCreateModalComponent
  extends BaseStudentGroupForm
  implements
    OnInit,
    ISubbmittable,
    ISuccessNotificatable,
    IFailureNotificatable
{
  @Output() visibility: EventEmitter<boolean> = new EventEmitter<boolean>();

  protected isSuccess: boolean;
  protected isFailure: boolean;

  public constructor(
    private readonly _facadeService: StudentGroupsFacadeService,
    protected readonly notificationService: UserOperationNotificationService
  ) {
    super();
    this.isFailure = false;
    this.isSuccess = false;
  }

  public submit(): void {
    const group = this.createGroupFromForm();
    console.log(group);
    const handler = StudentGroupCreationHandler(
      this._facadeService,
      this.notificationService,
      this,
      this
    );

    this._facadeService
      .create(group)
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
  }

  public notifyFailure(): void {
    this.isFailure = true;
  }

  public notifySuccess(): void {
    this.isSuccess = true;
  }

  public manageNotification(value: boolean): void {
    this.isSuccess = value;
    this.isFailure = value;
  }

  public ngOnInit(): void {
    this.initForm();
  }

  protected close(): void {
    this.visibility.emit(false);
  }
}
