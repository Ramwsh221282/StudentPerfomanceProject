import { Component, EventEmitter, Output } from '@angular/core';
import { User } from '../../../../../users/services/user-interface';
import { UserCreationService } from '../../services/user-create.service';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { StringValueBuilder } from '../../../../../../shared/models/form-value-builder/string-value-builder';
import { ISubbmittable } from '../../../../../../shared/models/interfaces/isubbmitable';
import { UserCreationHandler } from './users-create-handler';
import { catchError, tap } from 'rxjs';

@Component({
  selector: 'app-users-create-modal',
  templateUrl: './users-create-modal.component.html',
  styleUrl: './users-create-modal.component.scss',
  providers: [UserCreationService],
})
export class UsersCreateModalComponent implements ISubbmittable {
  @Output() visibilityEmitter: EventEmitter<boolean> =
    new EventEmitter<boolean>();
  @Output() refreshEmitter: EventEmitter<void> = new EventEmitter<void>();
  @Output() successEmitter: EventEmitter<void> = new EventEmitter<void>();
  @Output() failureEmitter: EventEmitter<void> = new EventEmitter<void>();

  protected user: User;

  public constructor(
    private readonly _creationService: UserCreationService,
    private readonly _notificationService: UserOperationNotificationService
  ) {
    this.initUser();
  }

  private initUser(): void {
    const builder: StringValueBuilder = new StringValueBuilder();
    this.user = {} as User;
    this.user.name = builder.extractStringOrEmpty(this.user.name);
    this.user.surname = builder.extractStringOrEmpty(this.user.surname);
    this.user.thirdname = builder.extractStringOrEmpty(this.user.thirdname);
    this.user.email = builder.extractStringOrEmpty(this.user.email);
    this.user.role = 'Выберите права пользователя';
    this.user.token = builder.extractStringOrEmpty(this.user.token);
  }

  public submit(): void {
    const handler = UserCreationHandler(
      this._notificationService,
      this.successEmitter,
      this.failureEmitter,
      this.refreshEmitter,
      this.visibilityEmitter
    );
    this._creationService
      .create(this.user)
      .pipe(
        tap((response) => handler.handle(response)),
        catchError((error) => handler.handleError(error))
      )
      .subscribe();
    this.initUser();
  }
}
