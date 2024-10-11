import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { EducationDirectionBaseForm } from '../../models/education-direction-base-form';
import { catchError, tap } from 'rxjs';
import { FacadeService } from '../../services/facade.service';
import { EducationDirectionCreationHandler } from './education-directions-create-handler';
import { HttpErrorResponse } from '@angular/common/http';
import { ISubbmittable } from '../../../../../../shared/models/interfaces/isubbmitable';
import { ISuccessNotificatable } from '../../../../../../shared/models/interfaces/isuccess-notificatable';
import { IFailureNotificatable } from '../../../../../../shared/models/interfaces/ifailure-notificatable';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';

@Component({
  selector: 'app-education-directions-create-modal',
  templateUrl: './education-directions-create-modal.component.html',
  styleUrl: './education-directions-create-modal.component.scss',
  providers: [UserOperationNotificationService],
})
export class EducationDirectionsCreateModalComponent
  extends EducationDirectionBaseForm
  implements
    OnInit,
    ISubbmittable,
    ISuccessNotificatable,
    IFailureNotificatable
{
  @Output() visibility: EventEmitter<boolean> = new EventEmitter<boolean>();

  protected isFailure: boolean;
  protected isSuccess: boolean;

  public constructor(
    private readonly _facadeService: FacadeService,
    protected readonly notificationService: UserOperationNotificationService
  ) {
    super();
    this.isFailure = false;
    this.isSuccess = false;
  }

  public notifyFailure(): void {
    this.isFailure = true;
  }

  public notifySuccess(): void {
    this.isSuccess = true;
  }

  public override submit(): void {
    const direction = this.createEducationDirectionFromForm();
    console.log(direction);
    const handler = EducationDirectionCreationHandler(
      this._facadeService,
      this.notificationService,
      this,
      this
    );

    this._facadeService
      .create(direction)
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
    this.ngOnInit();
  }

  public ngOnInit(): void {
    this.initForm();
  }

  public close(): void {
    this.ngOnInit();
    this.visibility.emit(false);
  }

  protected manageNotification(value: boolean) {
    this.isSuccess = value;
    this.isFailure = value;
  }
}
