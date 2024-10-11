import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { FacadeService } from '../../services/facade.service';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { EducationDirectionBaseForm } from '../../models/education-direction-base-form';
import { ISuccessNotificatable } from '../../../../../../shared/models/interfaces/isuccess-notificatable';
import { IFailureNotificatable } from '../../../../../../shared/models/interfaces/ifailure-notificatable';
import { EducationDirection } from '../../models/education-direction-interface';
import { CreateEducationDirectionEditHandler } from './education-direction-edit-handler';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-education-directions-edit-modal',
  templateUrl: './education-directions-edit-modal.component.html',
  styleUrl: './education-directions-edit-modal.component.scss',
  providers: [BsModalService],
})
export class EducationDirectionsEditModalComponent
  extends EducationDirectionBaseForm
  implements OnInit, ISuccessNotificatable, IFailureNotificatable
{
  @Input({ required: true }) direction: EducationDirection;
  @Input({ required: true }) copy: EducationDirection;
  @Output() modalDisabled: EventEmitter<boolean> = new EventEmitter<boolean>();

  protected isSuccess: boolean;
  protected isFailure: boolean;

  public constructor(
    protected readonly facadeService: FacadeService,
    protected readonly notificationService: UserOperationNotificationService
  ) {
    super();
    this.isSuccess = false;
    this.isFailure = false;
  }

  public notifyFailure(): void {
    this.isFailure = true;
  }

  public notifySuccess(): void {
    this.isSuccess = true;
  }

  public ngOnInit(): void {
    this.initForm();
  }

  public override submit(): void {
    const newDirection = this.createEducationDirectionFromForm();
    const handler = CreateEducationDirectionEditHandler(
      this.copy,
      this.facadeService,
      this.notificationService,
      this,
      this
    );
    this.facadeService
      .update(this.copy, newDirection)
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
  }

  protected close(): void {
    this.ngOnInit();
    this.modalDisabled.emit(false);
  }

  protected manageNotification(value: boolean): void {
    this.isSuccess = value;
    this.isFailure = value;
  }
}
