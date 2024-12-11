import { Component, OnInit } from '@angular/core';
import { UsersDataService } from '../../services/users-data.service';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { UserRecord } from '../../services/user-table-element-interface';
import { catchError, Observable, tap } from 'rxjs';
import { DatePipe } from '@angular/common';
import { UsersPaginationService } from './users-table-pagination/users-pagination.servce';
import { DefaultFetchPolicy } from '../../models/users-fetch-policies/users-default-fetch-policy';
import { AuthService } from '../../../../../users/services/auth.service';
import { AppConfigService } from '../../../../../../app.config.service';

@Component({
  selector: 'app-users-table',
  templateUrl: './users-table.component.html',
  styleUrl: './users-table.component.scss',
  providers: [
    UsersPaginationService,
    UsersDataService,
    UserOperationNotificationService,
    DatePipe,
  ],
})
export class UsersTableComponent implements OnInit {
  protected userRecords: UserRecord[];

  protected activeUser: UserRecord;
  protected isSuccess: boolean;
  protected isFailure: boolean;

  protected isCreationModalVisible: boolean;
  protected isDeletionModalVisible: boolean;
  protected isFilterModalVisible: boolean;

  public constructor(
    private readonly _datePipe: DatePipe,
    protected readonly _dataService: UsersDataService,
    protected readonly _notificationService: UserOperationNotificationService,
    private readonly _authService: AuthService,
    private readonly _paginationService: UsersPaginationService,
    private readonly _appConfig: AppConfigService,
  ) {
    this.userRecords = [];
    this.isSuccess = false;
    this.isFailure = false;
    this.isCreationModalVisible = false;
    this.isDeletionModalVisible = false;
    this.activeUser = {} as UserRecord;
  }

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

  protected refreshActiveUser(): void {
    this.activeUser = {} as UserRecord;
  }

  protected fetchUsers(): void {
    this._dataService
      .fetch()
      .pipe(
        tap((response) => {
          this.userRecords = response;
          for (let user of this.userRecords) {
            user.lastTimeOnline = this._datePipe.transform(
              user.lastTimeOnline,
              'dd-MM-yyyy',
            );
            user.registeredDate = this._datePipe.transform(
              user.registeredDate,
              'dd-MM-yyyy',
            );
          }
        }),
        catchError((error) => {
          this._notificationService.SetMessage = error.error;
          this.isFailure = true;
          return new Observable();
        }),
      )
      .subscribe();
    this._paginationService.refreshPagination();
  }

  protected fetchOnPageChanged(): void {
    this._dataService
      .fetch()
      .pipe(
        tap((response) => {
          this.userRecords = response;
          for (let user of this.userRecords) {
            user.lastTimeOnline = this._datePipe.transform(
              user.lastTimeOnline,
              'dd-MM-yyyy',
            );
            user.registeredDate = this._datePipe.transform(
              user.registeredDate,
              'dd-MM-yyyy',
            );
          }
        }),
        catchError((error) => {
          this._notificationService.SetMessage = error.error;
          this.isFailure = true;
          return new Observable();
        }),
      )
      .subscribe();
  }
}
