import { Component, EventEmitter, Output } from '@angular/core';
import { UserPasswordUpdateService } from './user-password-update-service';
import { UserOperationNotificationService } from '../../../shared/services/user-notifications/user-operation-notification-service.service';
import { ISubbmittable } from '../../../shared/models/interfaces/isubbmitable';
import { UserPasswordUpdateHandler } from './user-password-update-handler';
import { catchError, tap } from 'rxjs';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-user-password-update-form',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './user-password-update-form.component.html',
  styleUrl: './user-password-update-form.component.scss',
  providers: [UserPasswordUpdateService],
})
export class UserPasswordUpdateFormComponent implements ISubbmittable {
  @Output() successEmitter: EventEmitter<void> = new EventEmitter();
  @Output() failureEmitter: EventEmitter<void> = new EventEmitter();
  protected currentPassword: string = '';
  protected updatedPassword: string = '';

  public constructor(
    private readonly _notificationService: UserOperationNotificationService,
    private readonly _passwordService: UserPasswordUpdateService,
  ) {}

  public submit(): void {
    const handler = UserPasswordUpdateHandler(this._notificationService);
    this._passwordService
      .requestPasswordUpdate(this.currentPassword, this.updatedPassword)
      .pipe(
        tap((response) => {
          handler.handle(response);
          this.currentPassword = '';
          this.updatedPassword = '';
        }),
        catchError((error) => handler.handleError(error)),
      )
      .subscribe();
  }
}
