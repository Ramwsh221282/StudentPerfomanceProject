import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FacadeService } from '../../services/facade.service';
import { EducationPlan } from '../../models/education-plan-interface';
import { EducationPlanDeletionHandler } from './education-plan-deletion-handler';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { ISubbmittable } from '../../../../../../shared/models/interfaces/isubbmitable';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';

@Component({
  selector: 'app-education-plan-deletion-modal',
  templateUrl: './education-plan-deletion-modal.component.html',
  styleUrl: './education-plan-deletion-modal.component.scss',
})
export class EducationPlanDeletionModalComponent implements ISubbmittable {
  @Input({ required: true }) plan: EducationPlan;
  @Output() refreshEmiiter: EventEmitter<void> = new EventEmitter();
  @Output() visibility: EventEmitter<boolean> = new EventEmitter<boolean>();

  public constructor(
    private readonly _facadeService: FacadeService,
    private readonly _notificationService: UserOperationNotificationService,
  ) {}

  public submit(): void {
    const handler = EducationPlanDeletionHandler(
      this._notificationService,
      this.refreshEmiiter,
      this.visibility,
    );
    this._facadeService
      .delete(this.plan)
      .pipe(
        tap((response) => {
          handler.handle(response);
          this.close();
        }),
        catchError((error: HttpErrorResponse) => {
          this.close();
          return handler.handleError(error);
        }),
      )
      .subscribe();
  }

  protected close(): void {
    this.visibility.emit(false);
  }
}
