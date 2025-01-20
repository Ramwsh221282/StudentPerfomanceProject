import { Component, OnInit } from '@angular/core';
import { UserCardComponent } from './user-card/user-card.component';
import { AuthService } from '../../../modules/users/services/auth.service';
import { UserCardOptionsComponent } from './user-card-options/user-card-options.component';
import { UserOperationNotificationService } from '../../services/user-notifications/user-operation-notification-service.service';
import { AdminShortInfoService } from './services/admin-short-info.service';
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
export class UserPageComponent implements OnInit {
  protected isEmailUpdateRequested: boolean = false;
  protected isPasswordUpdateRequested: boolean = false;

  public constructor(
    protected readonly authService: AuthService,
    private readonly _adminShortInfo: AdminShortInfoService,
  ) {}

  public ngOnInit() {
    this._adminShortInfo.invokeGetInfo();
  }
}
