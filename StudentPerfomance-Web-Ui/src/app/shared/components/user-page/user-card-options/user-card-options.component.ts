import { Component } from '@angular/core';
import { UserPasswordUpdateFormComponent } from '../user-password-update-form/user-password-update-form.component';
import { UserEmailUpdateFormComponent } from '../user-email-update-form/user-email-update-form.component';
import { NgIf } from '@angular/common';
import { UserOperationNotificationService } from '../../../services/user-notifications/user-operation-notification-service.service';
import { SuccessResultNotificationComponent } from '../../success-result-notification/success-result-notification.component';
import { FailureResultNotificationComponent } from '../../failure-result-notification/failure-result-notification.component';

@Component({
  selector: 'app-user-card-options',
  standalone: true,
  imports: [
    UserPasswordUpdateFormComponent,
    UserEmailUpdateFormComponent,
    NgIf,
    SuccessResultNotificationComponent,
    FailureResultNotificationComponent,
  ],
  templateUrl: './user-card-options.component.html',
  styleUrl: './user-card-options.component.scss',
  providers: [UserOperationNotificationService],
})
export class UserCardOptionsComponent {
  protected isEmailUpdateContainerVisible: boolean = false;
  protected isPasswordUpdateContainerVisible: boolean = false;
  protected isSuccess = false;
  protected isFailure = false;

  public constructor(
    protected readonly notificationService: UserOperationNotificationService,
  ) {}
}
