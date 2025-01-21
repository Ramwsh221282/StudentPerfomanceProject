import { Component, EventEmitter, Output } from '@angular/core';
import { SessionReportsDataService } from '../Services/data-services/session-reports-data-service';
import { AuthService } from '../../../../pages/user-page/services/auth.service';
import { SessionReportsPaginationService } from '../session-reports-pagination/session-reports-pagination-service';
import { ISubbmittable } from '../../../models/interfaces/isubbmitable';
import { FormsModule } from '@angular/forms';
import { IFetchPolicy } from '../../../models/fetch-policices/fetch-policy-interface';
import { ControlWeekReportInterface } from '../Models/Data/control-week-report-interface';
import { SessionReportPeriodFetchPolicy } from '../Services/data-services/reports-fetch-policies/session-report-period-fetch-policy';
import { SessionReportDefaultFetchPolicy } from '../Services/data-services/reports-fetch-policies/session-report-default-fetch-policy';
import { AppConfigService } from '../../../../app.config.service';

@Component({
    selector: 'app-session-reports-filter-input',
    imports: [FormsModule],
    templateUrl: './session-reports-filter-input.component.html',
    styleUrl: './session-reports-filter-input.component.scss'
})
export class SessionReportsFilterInputComponent implements ISubbmittable {
  @Output() emitFilter: EventEmitter<void> = new EventEmitter();
  protected year: number;
  protected sessionNumber: number;
  protected sessionSeason: string;

  public constructor(
    private readonly _dataService: SessionReportsDataService,
    private readonly _paginationService: SessionReportsPaginationService,
    private readonly _authService: AuthService,
    private readonly _appConfig: AppConfigService,
  ) {}

  public submit(): void {
    const policy: IFetchPolicy<ControlWeekReportInterface[]> =
      new SessionReportPeriodFetchPolicy(
        this._authService,
        this.year,
        this.sessionNumber,
        this.sessionSeason,
        this._appConfig,
      );
    policy.addPages(
      this._paginationService.currentPage,
      this._paginationService.pageSize,
    );
    this._dataService.setPolicy(policy);
    this.emitFilter.emit();
  }

  public cancelFilter(): void {
    const policy: IFetchPolicy<ControlWeekReportInterface[]> =
      new SessionReportDefaultFetchPolicy(this._authService, this._appConfig);
    policy.addPages(
      this._paginationService.currentPage,
      this._paginationService.pageSize,
    );
    this.sessionNumber = null!;
    this.sessionSeason = '';
    this.year = null!;
    this._dataService.setPolicy(policy);
    this.emitFilter.emit();
  }
}
