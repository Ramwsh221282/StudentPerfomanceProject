import { Component, EventEmitter, Output } from '@angular/core';
import { UserOperationNotificationService } from '../../../services/user-notifications/user-operation-notification-service.service';
import { ISubbmittable } from '../../../models/interfaces/isubbmitable';
import { UserEmailUpdateService } from './user-email-update-service';
import { UserEmailUpdateHandler } from './user-email-update-handler';
import { catchError, tap } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../../../modules/users/services/auth.service';

@Component({
  selector: 'app-user-email-update-form',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './user-email-update-form.component.html',
  styleUrl: './user-email-update-form.component.scss',
  providers: [UserEmailUpdateService],
})
export class UserEmailUpdateFormComponent implements ISubbmittable {
  @Output() successEmitter: EventEmitter<void> = new EventEmitter();
  @Output() failureEmitter: EventEmitter<void> = new EventEmitter();
  protected currentPassword: string = '';
  protected Email: string = '';

  public constructor(
    private readonly _notificationService: UserOperationNotificationService,
    private readonly _emailService: UserEmailUpdateService,
    private readonly _authService: AuthService,
  ) {}

  public submit(): void {
    const handler = UserEmailUpdateHandler(this._notificationService);
    this._emailService
      .requestEmailUpdate(this.currentPassword, this.Email)
      .pipe(
        tap((response) => {
          handler.handle(response);
          this.currentPassword = '';
          this.Email = '';
          this._authService.userData.email = response.email;
        }),
        catchError((error) => handler.handleError(error)),
      )
      .subscribe();
  }
}
