import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { EducationDirectionBaseForm } from '../../models/education-direction-base-form';
import { catchError, tap } from 'rxjs';
import { FacadeService } from '../../services/facade.service';
import { EducationDirectionCreationHandler } from './education-directions-create-handler';
import { HttpErrorResponse } from '@angular/common/http';
import { ISubbmittable } from '../../../../../../shared/models/interfaces/isubbmitable';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';

@Component({
  selector: 'app-education-directions-create-modal',
  templateUrl: './education-directions-create-modal.component.html',
  styleUrl: './education-directions-create-modal.component.scss',
})
export class EducationDirectionsCreateModalComponent
  extends EducationDirectionBaseForm
  implements OnInit, ISubbmittable
{
  @Output() visibility: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output() successEmitter: EventEmitter<void> = new EventEmitter();
  @Output() failureEmitter: EventEmitter<void> = new EventEmitter();
  @Output() refreshEmitter: EventEmitter<void> = new EventEmitter();

  public constructor(
    private readonly _facadeService: FacadeService,
    protected readonly notificationService: UserOperationNotificationService
  ) {
    super();
  }

  public override submit(): void {
    const direction = this.createEducationDirectionFromForm();
    const handler = EducationDirectionCreationHandler(
      this._facadeService,
      this.notificationService,
      this.successEmitter,
      this.failureEmitter,
      this.visibility,
      this.refreshEmitter
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
}
