import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FacadeService } from '../../services/facade.service';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { EducationDirectionBaseForm } from '../../models/education-direction-base-form';
import { EducationDirection } from '../../models/education-direction-interface';
import { CreateEducationDirectionEditHandler } from './education-direction-edit-handler';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { ISubbmittable } from '../../../../../../shared/models/interfaces/isubbmitable';

@Component({
  selector: 'app-education-directions-edit-modal',
  templateUrl: './education-directions-edit-modal.component.html',
  styleUrl: './education-directions-edit-modal.component.scss',
})
export class EducationDirectionsEditModalComponent
  extends EducationDirectionBaseForm
  implements OnInit, ISubbmittable
{
  @Input({ required: true }) direction: EducationDirection;
  @Input({ required: true }) copy: EducationDirection;
  @Output() modalDisabled: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output() refreshEmitter: EventEmitter<void> = new EventEmitter();
  @Output() successEmitter: EventEmitter<void> = new EventEmitter();
  @Output() failureEmitter: EventEmitter<void> = new EventEmitter();

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

  public ngOnInit(): void {
    this.initForm();
  }

  public submit(): void {
    const newDirection = this.createEducationDirectionFromForm();
    const handler = CreateEducationDirectionEditHandler(
      this.copy,
      this.facadeService,
      this.notificationService,
      this.successEmitter,
      this.failureEmitter,
      this.refreshEmitter,
      this.modalDisabled
    );
    this.facadeService
      .update(this.copy, newDirection)
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
  }
}
