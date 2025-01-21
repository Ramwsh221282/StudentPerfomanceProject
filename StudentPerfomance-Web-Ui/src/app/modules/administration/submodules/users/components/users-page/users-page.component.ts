import { Component, OnInit } from '@angular/core';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { UsersPaginationService } from '../users-table/users-table-pagination/users-pagination.servce';
import { UsersDataService } from '../../services/users-data.service';
import { DatePipe } from '@angular/common';
import { UserRecord } from '../../services/user-table-element-interface';
import { DefaultFetchPolicy } from '../../models/users-fetch-policies/users-default-fetch-policy';
import { AuthService } from '../../../../../../pages/user-page/services/auth.service';
import { AppConfigService } from '../../../../../../app.config.service';

@Component({
  selector: 'app-users-page',
  templateUrl: './users-page.component.html',
  styleUrl: './users-page.component.scss',
  providers: [
    UserOperationNotificationService,
    UsersPaginationService,
    UsersDataService,
    DatePipe,
  ],
})
export class UsersPageComponent implements OnInit {
  protected users: UserRecord[];

  public constructor(
    private readonly _dataService: UsersDataService,
    private readonly _paginationService: UsersPaginationService,
    private readonly _authService: AuthService,
    private readonly _appConfig: AppConfigService,
    protected readonly notificationService: UserOperationNotificationService,
  ) {}

  public ngOnInit(): void {
    const policy = new DefaultFetchPolicy(
      this._authService.userData,
      this._appConfig,
    );
    policy.addPages(
      this._paginationService.currentPage,
      this._paginationService.pageSize,
    );
    this.fetchUsers();
  }

  protected fetchUsers(): void {
    this._dataService.fetch().subscribe((response) => {
      this.users = response;
    });
  }

  protected handleFilter(): void {
    this._dataService.fetch().subscribe((response) => {
      this.users = response;
    });
  }

  protected handleUserCreation(): void {
    this._paginationService.refreshPagination();
    this._dataService.fetch().subscribe((response) => {
      this.users = response;
    });
  }

  protected handleUserRemove(): void {
    this.handleUserCreation();
  }
}
