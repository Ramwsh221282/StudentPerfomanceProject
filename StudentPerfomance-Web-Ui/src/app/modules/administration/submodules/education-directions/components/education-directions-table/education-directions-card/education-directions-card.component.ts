import { Component, Input } from '@angular/core';
import { EducationDirection } from '../../../models/education-direction-interface';
import { FacadeService } from '../../../services/facade.service';
import { ModalState } from '../../../../../../../shared/models/modals/modal-state';
import { INotificatable } from '../../../../../../../shared/models/inotificated-component-interface/inotificatable.interface';
import { EducationDirectionDeletionHandler } from './education-direction-deletion-handler';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-education-directions-card',
  templateUrl: './education-directions-card.component.html',
  styleUrl: './education-directions-card.component.scss',
})
export class EducationDirectionsCardComponent implements INotificatable {
  protected readonly deletionConfirmationModalState: ModalState;
  protected readonly editModalState: ModalState;
  public readonly successModalState: ModalState;
  public readonly failureModalState: ModalState;
  @Input({ required: true }) public direction: EducationDirection;
  public constructor(
    protected readonly facadeService: FacadeService,
    private readonly _notificationService: UserOperationNotificationService
  ) {
    this.deletionConfirmationModalState = new ModalState();
    this.editModalState = new ModalState();
    this.successModalState = new ModalState();
    this.failureModalState = new ModalState();
  }

  protected startDeletionTransaction(): void {
    this.deletionConfirmationModalState.turnOn();
  }

  protected startUpdateTransaction(): void {
    this.facadeService.setSelection = this.direction;
    this.editModalState.turnOn();
  }

  protected processDeletionTransaction(confirmation: boolean) {
    this.deletionConfirmationModalState.turnOff();
    if (confirmation) {
      const handler = EducationDirectionDeletionHandler(
        this.facadeService,
        this._notificationService,
        this
      );
      this.facadeService
        .delete(this.direction)
        .pipe(
          tap((response) => handler.handle(response)),
          catchError((error: HttpErrorResponse) => handler.handleError(error))
        )
        .subscribe();
    }
  }
}
