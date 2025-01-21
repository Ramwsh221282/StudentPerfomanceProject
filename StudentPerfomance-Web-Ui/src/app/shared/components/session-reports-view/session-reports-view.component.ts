import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { SessionReportsLayoutComponent } from './session-reports-layout/session-reports-layout.component';
import { SessionReportsFilterInputComponent } from './session-reports-filter-input/session-reports-filter-input.component';
import { SessionReportsDataService } from './Services/data-services/session-reports-data-service';
import { ControlWeekReportInterface } from './Models/Data/control-week-report-interface';
import { SessionReportDefaultFetchPolicy } from './Services/data-services/reports-fetch-policies/session-report-default-fetch-policy';
import { AuthService } from '../../../pages/user-page/services/auth.service';
import { IFetchPolicy } from '../../models/fetch-policices/fetch-policy-interface';
import { DatePipe, NgIf } from '@angular/common';
import { SessionReportsPaginationService } from './session-reports-pagination/session-reports-pagination-service';
import { SessionReportsPaginationComponent } from './session-reports-pagination/session-reports-pagination.component';
import { AppConfigService } from '../../../app.config.service';
import { UserOperationNotificationService } from '../../services/user-notifications/user-operation-notification-service.service';
import { SuccessResultNotificationComponent } from '../success-result-notification/success-result-notification.component';
import { FailureResultNotificationComponent } from '../failure-result-notification/failure-result-notification.component';

@Component({
  selector: 'app-session-reports-view',
  standalone: true,
  imports: [
    RouterLink,
    SessionReportsLayoutComponent,
    SessionReportsFilterInputComponent,
    SessionReportsPaginationComponent,
    NgIf,
    SuccessResultNotificationComponent,
    FailureResultNotificationComponent,
  ],
  templateUrl: './session-reports-view.component.html',
  styleUrl: './session-reports-view.component.scss',
  providers: [
    SessionReportsPaginationService,
    SessionReportsDataService,
    DatePipe,
    UserOperationNotificationService,
  ],
})
export class SessionReportsViewComponent implements OnInit {
  protected reports: ControlWeekReportInterface[];

  protected isSuccess: boolean;
  protected isFailure: boolean;

  public constructor(
    private readonly _paginationService: SessionReportsPaginationService,
    private readonly _dataService: SessionReportsDataService,
    private readonly _authService: AuthService,
    private readonly _appConfig: AppConfigService,
    protected readonly notificationService: UserOperationNotificationService,
  ) {
    this.reports = [];
  }

  public ngOnInit(): void {
    const policy: IFetchPolicy<ControlWeekReportInterface[]> =
      new SessionReportDefaultFetchPolicy(this._authService, this._appConfig);
    policy.addPages(
      this._paginationService.currentPage,
      this._paginationService.pageSize,
    );
    this._dataService.setPolicy(policy);
    this.fetchData();
  }

  protected fetchOnPageChanged(): void {
    this._dataService.addPages(
      this._paginationService.currentPage,
      this._paginationService.pageSize,
    );
    this.fetchData();
  }

  protected refreshPagination(): void {
    this._paginationService.refreshPagination();
  }

  protected fetchData(): void {
    this._dataService.fetch().subscribe((response) => {
      this.reports = response;
    });
  }
}
