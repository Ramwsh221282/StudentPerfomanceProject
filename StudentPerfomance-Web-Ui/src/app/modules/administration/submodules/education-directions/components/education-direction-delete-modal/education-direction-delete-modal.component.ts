import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FacadeService } from '../../services/facade.service';
import { EducationDirection } from '../../models/education-direction-interface';
import { EducationDirectionDeletionHandler } from './education-direction-deletion-handler';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-education-direction-delete-modal',
  templateUrl: './education-direction-delete-modal.component.html',
  styleUrl: './education-direction-delete-modal.component.scss',
})
export class EducationDirectionDeleteModalComponent {
  @Input({ required: true }) direction: EducationDirection;
  @Output() refreshEmitter: EventEmitter<void> = new EventEmitter();
  @Output() successEmitter: EventEmitter<void> = new EventEmitter();
  @Output() failureEmitter: EventEmitter<void> = new EventEmitter();
  @Output() visibility: EventEmitter<boolean> = new EventEmitter();

  public constructor(
    private readonly _facadeService: FacadeService,
    protected readonly notificationService: UserOperationNotificationService
  ) {}

  protected confirm(): void {
    const handler = EducationDirectionDeletionHandler(
      this._facadeService,
      this.notificationService,
      this.successEmitter,
      this.failureEmitter,
      this.visibility,
      this.refreshEmitter
    );
    this._facadeService
      .delete(this.direction)
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error: HttpErrorResponse) => handler.handleError(error))
      )
      .subscribe();
  }
}
