import { Component } from '@angular/core';
import { UserCardComponent } from './user-card/user-card.component';
import { AuthService } from './services/auth.service';
import { UserCardOptionsComponent } from './user-card-options/user-card-options.component';
import { UserOperationNotificationService } from '../../shared/services/user-notifications/user-operation-notification-service.service';
import { AdminUserShortInfoComponent } from './admin-user-short-info/admin-user-short-info.component';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-user-page',
  standalone: true,
  imports: [
    UserCardComponent,
    UserCardOptionsComponent,
    AdminUserShortInfoComponent,
    NgIf,
  ],
  templateUrl: './user-page.component.html',
  styleUrl: './user-page.component.scss',
  providers: [UserOperationNotificationService],
})
export class UserPageComponent {
  protected isEmailUpdateRequested: boolean = false;
  protected isPasswordUpdateRequested: boolean = false;

  public constructor(protected readonly authService: AuthService) {}
}
