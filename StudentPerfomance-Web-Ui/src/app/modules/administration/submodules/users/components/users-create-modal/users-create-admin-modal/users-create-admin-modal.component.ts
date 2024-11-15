import { Component, EventEmitter, Output } from '@angular/core';
import { ISubbmittable } from '../../../../../../../shared/models/interfaces/isubbmitable';
import { UserCreationService } from '../../../services/user-create.service';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { User } from '../../../../../../users/services/user-interface';
import { StringValueBuilder } from '../../../../../../../shared/models/form-value-builder/string-value-builder';
import { UserCreationHandler } from '../users-create-handler';
import { catchError, tap } from 'rxjs';

@Component({
  selector: 'app-users-create-admin-modal',
  templateUrl: './users-create-admin-modal.component.html',
  styleUrl: './users-create-admin-modal.component.scss',
  providers: [UserCreationService],
})
export class UsersCreateAdminModalComponent implements ISubbmittable {
  @Output() visibility: EventEmitter<boolean> = new EventEmitter();
  @Output() success: EventEmitter<void> = new EventEmitter();
  @Output() failure: EventEmitter<void> = new EventEmitter();
  @Output() refreshEmitter: EventEmitter<void> = new EventEmitter();

  protected user: User;

  public constructor(
    private readonly _creationService: UserCreationService,
    private readonly _notificationService: UserOperationNotificationService,
  ) {
    const builder: StringValueBuilder = new StringValueBuilder();
    this.initUser();
  }

  private initUser(): void {
    const builder: StringValueBuilder = new StringValueBuilder();
    this.user = {} as User;
    this.user.name = builder.extractStringOrEmpty(this.user.name);
    this.user.surname = builder.extractStringOrEmpty(this.user.surname);
    this.user.patronymic = builder.extractStringOrEmpty(this.user.patronymic);
    this.user.email = builder.extractStringOrEmpty(this.user.email);
    this.user.role = 'Администратор';
    this.user.token = builder.extractStringOrEmpty(this.user.token);
  }

  public submit(): void {
    const handler = UserCreationHandler(
      this._notificationService,
      this.success,
      this.failure,
      this.refreshEmitter,
      this.visibility,
    );
    this._creationService
      .create(this.user)
      .pipe(
        tap((response) => {
          handler.handle(response);
          this.refreshEmitter.emit();
          this.visibility.emit(false);
        }),
        catchError((error) => handler.handleError(error)),
      )
      .subscribe();
    this.initUser();
  }
}
