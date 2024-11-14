import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AssignmentSession } from '../../../../../models/assignment-session-interface';
import { UserOperationNotificationService } from '../../../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { AssignmentSessionCloseService } from './assignment-session-close.service';
import { ISubbmittable } from '../../../../../../../../../shared/models/interfaces/isubbmitable';
import { AssignmentSessionClosingHandler } from './assignment-session-closing.handler';
import { catchError, tap } from 'rxjs';

@Component({
  selector: 'app-assignment-session-close-item-modal',
  templateUrl: './assignment-session-close-item-modal.component.html',
  styleUrl: './assignment-session-close-item-modal.component.scss',
  providers: [AssignmentSessionCloseService],
})
export class AssignmentSessionCloseItemModalComponent implements ISubbmittable {
  @Input({ required: true }) session: AssignmentSession;
  @Output() visibility: EventEmitter<void> = new EventEmitter();
  @Output() success: EventEmitter<void> = new EventEmitter();
  @Output() failure: EventEmitter<void> = new EventEmitter();
  @Output() refresh: EventEmitter<void> = new EventEmitter();

  public constructor(
    private readonly _closingService: AssignmentSessionCloseService,
    protected readonly notificationService: UserOperationNotificationService,
  ) {}

  public submit(): void {
    const handler = AssignmentSessionClosingHandler(
      this.notificationService,
      this.success,
      this.failure,
      this.refresh,
    );
    this._closingService
      .close(this.session)
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error) => handler.handleError(error)),
      )
      .subscribe();
  }
}
