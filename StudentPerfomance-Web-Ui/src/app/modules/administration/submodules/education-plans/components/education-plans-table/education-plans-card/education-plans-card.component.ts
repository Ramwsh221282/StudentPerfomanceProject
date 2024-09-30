import { Component, Input } from '@angular/core';
import { EducationPlan } from '../../../models/education-plan-interface';
import { ModalState } from '../../../../../../../shared/models/modals/modal-state';
import { FacadeService } from '../../../services/facade.service';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { EducationPlanDeletionHandler } from './education-plans-deletion-handler';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { INotificatable } from '../../../../../../../shared/models/inotificated-component-interface/inotificatable.interface';

@Component({
  selector: 'app-education-plans-card',
  templateUrl: './education-plans-card.component.html',
  styleUrl: './education-plans-card.component.scss',
})
export class EducationPlansCardComponent implements INotificatable {
  protected readonly deletionConfirmationModalState: ModalState;
  public readonly successModalState: ModalState;
  public readonly failureModalState: ModalState;
  @Input({ required: true }) public plan: EducationPlan;

  public constructor(
    private readonly _facadeService: FacadeService,
    private readonly _notificationService: UserOperationNotificationService
  ) {
    this.successModalState = new ModalState();
    this.failureModalState = new ModalState();
    this.deletionConfirmationModalState = new ModalState();
  }

  protected startDeleteTransaction(): void {
    this.deletionConfirmationModalState.turnOn();
  }

  protected processDeletion(confirmation: boolean): void {
    this.deletionConfirmationModalState.turnOff();
    if (confirmation) {
      const handler = EducationPlanDeletionHandler(
        this._facadeService,
        this._notificationService,
        this
      );
      this._facadeService
        .delete(this.plan)
        .pipe(
          tap((response) => handler.handle(response)),
          catchError((error: HttpErrorResponse) => handler.handleError(error))
        )
        .subscribe();
    }
  }
}
