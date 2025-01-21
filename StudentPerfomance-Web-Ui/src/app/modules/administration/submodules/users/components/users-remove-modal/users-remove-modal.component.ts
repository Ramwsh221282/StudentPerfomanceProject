import { Component, EventEmitter, Input, Output } from '@angular/core';
import { UserRecord } from '../../services/user-table-element-interface';
import { ISubbmittable } from '../../../../../../shared/models/interfaces/isubbmitable';
import { UserRemoveService } from './user-remove.service';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { catchError, Observable, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    selector: 'app-users-remove-modal',
    templateUrl: './users-remove-modal.component.html',
    styleUrl: './users-remove-modal.component.scss',
    providers: [UserRemoveService],
    standalone: false
})
export class UsersRemoveModalComponent implements ISubbmittable {
  @Input({ required: true }) user: UserRecord;
  @Input({ required: true }) visibility: boolean = false;
  @Output() visibilityEmitter: EventEmitter<boolean> = new EventEmitter();
  @Output() removeCommited: EventEmitter<void> = new EventEmitter();

  public constructor(
    private readonly _removeService: UserRemoveService,
    private readonly _notificationService: UserOperationNotificationService,
  ) {}

  public submit(): void {
    const message = `Удален пользователь ${this.user.surname} ${this.user.name} ${this.user.patronymic} ${this.user.role}`;
    this._removeService
      .remove(this.user)
      .pipe(
        tap((response) => {
          this._notificationService.SetMessage = message;
          this._notificationService.success();
          this.removeCommited.emit();
          this.close();
        }),
        catchError((error: HttpErrorResponse) => {
          this._notificationService.SetMessage = error.error;
          this._notificationService.failure();
          this.close();
          return new Observable<never>();
        }),
      )
      .subscribe();
  }

  protected close(): void {
    this.visibility = false;
    this.visibilityEmitter.emit(this.visibility);
  }
}
