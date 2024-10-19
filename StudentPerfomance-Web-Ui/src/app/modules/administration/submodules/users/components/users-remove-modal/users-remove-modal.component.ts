import { Component, EventEmitter, Input, Output } from '@angular/core';
import { UserRecord } from '../../services/user-table-element-interface';
import { ISubbmittable } from '../../../../../../shared/models/interfaces/isubbmitable';
import { UserRemoveService } from './user-remove.service';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { UserRemoveHandler } from './user-remove-handler';
import { catchError, tap } from 'rxjs';

@Component({
  selector: 'app-users-remove-modal',
  templateUrl: './users-remove-modal.component.html',
  styleUrl: './users-remove-modal.component.scss',
  providers: [UserRemoveService],
})
export class UsersRemoveModalComponent implements ISubbmittable {
  @Input({ required: true }) user: UserRecord;
  @Output() visibilityEmitter: EventEmitter<boolean> = new EventEmitter();
  @Output() successEmitter: EventEmitter<void> = new EventEmitter();
  @Output() failureEmitter: EventEmitter<void> = new EventEmitter();
  @Output() refreshEmitter: EventEmitter<void> = new EventEmitter();

  public constructor(
    private readonly _removeService: UserRemoveService,
    private readonly _notificationService: UserOperationNotificationService
  ) {}

  public submit(): void {
    const handler = UserRemoveHandler(
      this._notificationService,
      this.successEmitter,
      this.failureEmitter,
      this.refreshEmitter,
      this.visibilityEmitter
    );
    this._removeService
      .remove(this.user)
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error) => handler.handleError(error))
      )
      .subscribe();
  }
}
