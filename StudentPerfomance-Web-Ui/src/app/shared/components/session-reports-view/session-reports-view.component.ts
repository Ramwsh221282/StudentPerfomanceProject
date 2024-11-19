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

@Component({
  selector: 'app-session-reports-view',
  standalone: true,
  imports: [
    RouterLink,
    SessionReportsLayoutComponent,
    SessionReportsFilterInputComponent,
  ],
  templateUrl: './session-reports-view.component.html',
  styleUrl: './session-reports-view.component.scss',
  providers: [SessionReportsDataService, DatePipe],
})
export class SessionReportsViewComponent implements OnInit {
  protected reports: ControlWeekReportInterface[];

  public constructor(
    private readonly _dataService: SessionReportsDataService,
    private readonly _authService: AuthService,
  ) {
    this.reports = [];
  }

  public ngOnInit(): void {
    const policy: IFetchPolicy<ControlWeekReportInterface[]> =
      new SessionReportDefaultFetchPolicy(this._authService);
    policy.addPages(1, 6);
    this._dataService.setPolicy(policy);
    this._dataService.fetch().subscribe((response) => {
      this.reports = response;
    });
  }
}
