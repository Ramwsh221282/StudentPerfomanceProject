import { Component, OnInit } from '@angular/core';
import { UserOperationNotificationService } from '../../../../../../../shared/services/user-notifications/user-operation-notification-service.service';
import { AssignmentSessionPaginationService } from './assignment-session-pagination.service';
import { AssignmentSessionDataService } from './assignment-session-data.service';
import { AssignmentSession } from '../../../models/assignment-session-interface';
import { PaginationService } from '../../../../education-directions/services/pagination.service';
import { AssignmentSessionDefaultFetchPolicy } from '../../../models/fetch-policies/assignment-session-default-fetch-policy';
import { AuthService } from '../../../../../../users/services/auth.service';
import { AppConfigService } from '../../../../../../../app.config.service';

@Component({
  selector: 'app-assignment-session-table',
  templateUrl: './assignment-session-table.component.html',
  styleUrl: './assignment-session-table.component.scss',
  providers: [
    UserOperationNotificationService,
    AssignmentSessionPaginationService,
    AssignmentSessionDataService,
  ],
})
export class AssignmentSessionTableComponent implements OnInit {
  protected _isCreationVisible: boolean;
  protected _isSuccess: boolean;
  protected _isFailure: boolean;

  protected sessions: AssignmentSession[];

  public constructor(
    protected readonly notificationService: UserOperationNotificationService,
    private readonly _paginationService: PaginationService,
    private readonly _dataService: AssignmentSessionDataService,
    private readonly _authService: AuthService,
    private readonly _appConfig: AppConfigService,
  ) {
    this.sessions = [];
  }

  public ngOnInit(): void {
    const policy = new AssignmentSessionDefaultFetchPolicy(
      this._authService,
      this._appConfig,
    );
    policy.addPages(
      this._paginationService.currentPage,
      this._paginationService.pageSize,
    );
    this._dataService.setPolicy(policy);
    this.fetchData();
  }

  protected refreshPagination(): void {
    this._paginationService.refreshPagination();
  }

  protected fetchData(): void {
    this._dataService.fetch().subscribe((response) => {
      this.sessions = response;
    });
  }
}
