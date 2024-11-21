import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { SessionReportsLayoutComponent } from './session-reports-layout/session-reports-layout.component';
import { SessionReportsFilterInputComponent } from './session-reports-filter-input/session-reports-filter-input.component';
import { SessionReportsDataService } from './Services/data-services/session-reports-data-service';
import { ControlWeekReportInterface } from './Models/Data/control-week-report-interface';
import { SessionReportDefaultFetchPolicy } from './Services/data-services/reports-fetch-policies/session-report-default-fetch-policy';
import { AuthService } from '../../../modules/users/services/auth.service';
import { IFetchPolicy } from '../../models/fetch-policices/fetch-policy-interface';
import { DatePipe } from '@angular/common';
import { SessionReportsPaginationService } from './session-reports-pagination/session-reports-pagination-service';
import { SessionReportsPaginationComponent } from './session-reports-pagination/session-reports-pagination.component';

@Component({
  selector: 'app-session-reports-view',
  standalone: true,
  imports: [
    RouterLink,
    SessionReportsLayoutComponent,
    SessionReportsFilterInputComponent,
    SessionReportsPaginationComponent,
  ],
  templateUrl: './session-reports-view.component.html',
  styleUrl: './session-reports-view.component.scss',
  providers: [
    SessionReportsPaginationService,
    SessionReportsDataService,
    DatePipe,
  ],
})
export class SessionReportsViewComponent implements OnInit {
  protected reports: ControlWeekReportInterface[];

  public constructor(
    private readonly _paginationService: SessionReportsPaginationService,
    private readonly _dataService: SessionReportsDataService,
    private readonly _authService: AuthService,
  ) {
    this.reports = [];
  }

  public ngOnInit(): void {
    const policy: IFetchPolicy<ControlWeekReportInterface[]> =
      new SessionReportDefaultFetchPolicy(this._authService);
    policy.addPages(
      this._paginationService.currentPage,
      this._paginationService.pageSize,
    );
    this._dataService.setPolicy(policy);
    this._dataService.fetch().subscribe((response) => {
      this.reports = response;
    });
  }

  protected fetchOnPageChanged(): void {
    this._dataService.addPages(
      this._paginationService.currentPage,
      this._paginationService.pageSize,
    );
    this._dataService.fetch().subscribe((response) => {
      this.reports = response;
    });
  }
}
