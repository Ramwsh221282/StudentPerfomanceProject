import { Component } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../../../../pages/user-page/services/auth.service';
import { UserOperationNotificationService } from '../../../services/user-notifications/user-operation-notification-service.service';
import { NgClass, NgForOf, NgIf } from '@angular/common';
import { SuccessResultNotificationComponent } from '../../success-result-notification/success-result-notification.component';
import { FailureResultNotificationComponent } from '../../failure-result-notification/failure-result-notification.component';
import { FloatingLabelInputComponent } from '../../../../building-blocks/floating-label-input/floating-label-input.component';
import { GreenOutlineButtonComponent } from '../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { YellowOutlineButtonComponent } from '../../../../building-blocks/buttons/yellow-outline-button/yellow-outline-button.component';
import { LoginHandler } from './login-handler';
import { catchError, Observable, tap } from 'rxjs';
import { PasswordRecoveryService } from './password-recovery-modal/password-recovery.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-form',
  imports: [
    ReactiveFormsModule,
    FormsModule,
    NgIf,
    SuccessResultNotificationComponent,
    FailureResultNotificationComponent,
    NgForOf,
    NgClass,
    FloatingLabelInputComponent,
    GreenOutlineButtonComponent,
    YellowOutlineButtonComponent,
  ],
  templateUrl: './login-form.component.html',
  styleUrl: './login-form.component.scss',
  providers: [UserOperationNotificationService],
})
export class LoginFormComponent {
  protected activeTab = 1;
  protected email: string = '';
  protected password: string = '';
  protected tabs: any = [
    {
      id: 1,
      label: 'Авторизация',
    },
    {
      id: 2,
      label: 'Восстановление пароля',
    },
  ];

  public constructor(
    private readonly _authService: AuthService,
    private readonly _recoveryService: PasswordRecoveryService,
    protected readonly notificationService: UserOperationNotificationService,
    private readonly _router: Router,
  ) {}

  protected loginClicked(): void {
    if (this.isEmailEmpty() || this.isPasswordEmpty()) {
      this.throwError();
      return;
    }
    const handler = LoginHandler(this.notificationService);
    this._authService
      .login({ email: this.email, password: this.password })
      .pipe(
        tap((response) => {
          handler.handle(response);
          this._authService.authorize(response);
          this.cleanInputs();
          this._router.navigate(['user']);
        }),
        catchError((error) => {
          this.cleanInputs();
          return handler.handleError(error);
        }),
      )
      .subscribe();
  }

  protected restoreClicked(): void {
    if (this.isEmailEmpty()) {
      this.throwError();
      return;
    }
    this._recoveryService
      .requestPasswordRecovery(this.email)
      .pipe(
        tap((response) => {
          this.notificationService.SetMessage =
            'Инструкции были отправлены на указанную почту';
          this.notificationService.success();
          this.cleanInputs();
        }),
        catchError((error) => {
          this.notificationService.SetMessage = error.error;
          this.notificationService.failure();
          this.cleanInputs();
          return new Observable();
        }),
      )
      .subscribe();
  }

  private isEmailEmpty(): boolean {
    return this.email.length == 0;
  }

  private isPasswordEmpty(): boolean {
    return this.password.length == 0;
  }

  private throwError(): void {
    this.notificationService.SetMessage =
      'Форма авторизации не заполнена полносьтю';
    this.notificationService.failure();
  }

  private cleanInputs(): void {
    this.email = '';
    this.password = '';
  }
}
