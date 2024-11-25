import { Component, OnInit } from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { AuthService } from '../../../../modules/users/services/auth.service';
import { ISubbmittable } from '../../../models/interfaces/isubbmitable';
import { UserOperationNotificationService } from '../../../services/user-notifications/user-operation-notification-service.service';
import { ISuccessNotificatable } from '../../../models/interfaces/isuccess-notificatable';
import { IFailureNotificatable } from '../../../models/interfaces/ifailure-notificatable';
import { catchError, tap } from 'rxjs';
import { LoginHandler } from './login-handler';
import { NgIf } from '@angular/common';
import { SuccessResultNotificationComponent } from '../../success-result-notification/success-result-notification.component';
import { FailureResultNotificationComponent } from '../../failure-result-notification/failure-result-notification.component';
import { PasswordRecoveryModalComponent } from './password-recovery-modal/password-recovery-modal.component';

@Component({
  selector: 'app-login-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    FormsModule,
    NgIf,
    SuccessResultNotificationComponent,
    FailureResultNotificationComponent,
    PasswordRecoveryModalComponent,
  ],
  templateUrl: './login-form.component.html',
  styleUrl: './login-form.component.scss',
  providers: [UserOperationNotificationService],
})
export class LoginFormComponent
  implements
    ISubbmittable,
    ISuccessNotificatable,
    IFailureNotificatable,
    OnInit
{
  protected form: FormGroup;
  public isSuccess: boolean;
  public isFailure: boolean;

  protected isRecoveryModalVisible: boolean = false;

  public constructor(
    private readonly _authService: AuthService,
    protected readonly notificationService: UserOperationNotificationService,
  ) {
    this.isSuccess = false;
    this.isFailure = false;
  }

  public notifyFailure(): void {
    this.isFailure = true;
  }

  public notifySuccess(): void {
    this.isSuccess = true;
  }

  public ngOnInit(): void {
    this.form = new FormGroup({
      email: new FormControl(null, [Validators.required]),
      password: new FormControl(null, [Validators.required]),
    });
  }

  public submit(): void {
    if (!this.form.valid) {
      this.isFailure = true;
      this.notificationService.SetMessage =
        'Некорректно заполнена форма авторизации';
      return;
    }
    const email: string = this.form.value['email'];
    const password: string = this.form.value['password'];
    const handler = LoginHandler(this.notificationService, this, this);
    this._authService
      .login({ email: email, password: password })
      .pipe(
        tap((response) => {
          handler.handle(response);
          this._authService.authorize(response);
        }),
        catchError((error) => handler.handleError(error)),
      )
      .subscribe();
    this.ngOnInit();
  }
}
