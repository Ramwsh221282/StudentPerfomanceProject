import { Component, Input } from '@angular/core';
import { User } from '../services/user-interface';
import { NgClass, NgForOf, NgIf } from '@angular/common';
import { FloatingLabelInputComponent } from '../../../building-blocks/floating-label-input/floating-label-input.component';
import { GreenOutlineButtonComponent } from '../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { UserPasswordUpdateService } from '../user-password-update-form/user-password-update-service';
import { UserEmailUpdateService } from '../user-email-update-form/user-email-update-service';
import { UserOperationNotificationService } from '../../../shared/services/user-notifications/user-operation-notification-service.service';
import { UserPasswordUpdateHandler } from '../user-password-update-form/user-password-update-handler';
import { catchError, tap } from 'rxjs';
import { SuccessResultNotificationComponent } from '../../../shared/components/success-result-notification/success-result-notification.component';
import { FailureResultNotificationComponent } from '../../../shared/components/failure-result-notification/failure-result-notification.component';
import { UserEmailUpdateHandler } from '../user-email-update-form/user-email-update-handler';
import { AuthService } from '../services/auth.service';
import { RedButtonComponent } from '../../../building-blocks/buttons/red-button/red-button.component';
import { Router } from '@angular/router';
import { animate, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-user-card',
  standalone: true,
  imports: [
    NgForOf,
    NgClass,
    FloatingLabelInputComponent,
    GreenOutlineButtonComponent,
    NgIf,
    SuccessResultNotificationComponent,
    FailureResultNotificationComponent,
    RedButtonComponent,
  ],
  templateUrl: './user-card.component.html',
  styleUrl: './user-card.component.scss',
  animations: [
    trigger('fadeIn', [
      transition(':enter', [
        style({ opacity: 0, transform: 'translateY(-10px)' }),
        animate(
          '300ms ease-out',
          style({ opacity: 1, transform: 'translateY(0)' }),
        ),
      ]),
      transition(':leave', [
        animate(
          '300ms ease-in',
          style({ opacity: 0, transform: 'translateY(-10px)' }),
        ),
      ]),
    ]),
  ],
})
export class UserCardComponent {
  protected readonly tabs = [
    {
      id: 1,
      label: 'Информация',
    },
    {
      id: 2,
      label: 'Сменить пароль',
    },
    {
      id: 3,
      label: 'Сменить почту',
    },
  ];

  protected activeTab: number = 1;

  protected currentPassword: string = '';
  protected newPassword: string = '';
  protected newEmail: string = '';

  @Input({ required: true }) user: User;

  public constructor(
    private readonly _passwordService: UserPasswordUpdateService,
    private readonly _emailService: UserEmailUpdateService,
    private readonly _authService: AuthService,
    protected readonly notificationService: UserOperationNotificationService,
    private readonly _router: Router,
  ) {}

  public dislogin(): void {
    this._authService.setNotAuthorized();
    this._router.navigate(['login']);
  }

  protected onPasswordUpdateClick(): void {
    if (this.isCurrentPasswordEmpty() || this.isNewPasswordEmpty()) {
      this.throwError('Форма смены пароля заполнена не полностью');
      return;
    }
    const handler = UserPasswordUpdateHandler(this.notificationService);
    this._passwordService
      .requestPasswordUpdate(this.currentPassword, this.newPassword)
      .pipe(
        tap((response) => {
          handler.handle(response);
          this.cleanInputs();
        }),
        catchError((error) => {
          this.cleanInputs();
          return handler.handleError(error);
        }),
      )
      .subscribe();
  }

  protected onEmailUpdateClick(): void {
    if (this.isCurrentPasswordEmpty() || this.isNewEmailEmpty()) {
      this.throwError('Форма смены почты заполнена не полностью');
      return;
    }
    const handler = UserEmailUpdateHandler(this.notificationService);
    this._emailService
      .requestEmailUpdate(this.currentPassword, this.newEmail)
      .pipe(
        tap((response) => {
          handler.handle(response);
          this.cleanInputs();
          this._authService.userData.email = response.email;
        }),
        catchError((error) => {
          this.cleanInputs();
          return handler.handleError(error);
        }),
      )
      .subscribe();
  }

  private isCurrentPasswordEmpty(): boolean {
    return this.currentPassword.length == 0;
  }

  private isNewPasswordEmpty(): boolean {
    return this.currentPassword.length == 0;
  }

  private isNewEmailEmpty(): boolean {
    return this.newEmail.length == 0;
  }

  private throwError(errorMessage: string): void {
    this.notificationService.SetMessage = errorMessage;
    this.notificationService.failure();
  }

  private cleanInputs(): void {
    this.currentPassword = '';
    this.newPassword = '';
    this.newEmail = '';
  }
}
