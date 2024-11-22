import { Component, EventEmitter, Output } from '@angular/core';
import { SessionReportPeriodContract } from '../Services/data-services/reports-fetch-policies/session-report-period-filter-contract';
import { SessionReportsDataService } from '../Services/data-services/session-reports-data-service';
import { AuthService } from '../../../../modules/users/services/auth.service';
import { SessionReportsPaginationService } from '../session-reports-pagination/session-reports-pagination-service';
import { ISubbmittable } from '../../../models/interfaces/isubbmitable';
import { FormsModule } from '@angular/forms';
import { IFetchPolicy } from '../../../models/fetch-policices/fetch-policy-interface';
import { ControlWeekReportInterface } from '../Models/Data/control-week-report-interface';
import { SessionReportPeriodFetchPolicy } from '../Services/data-services/reports-fetch-policies/session-report-period-fetch-policy';

@Component({
  selector: 'app-session-reports-filter-input',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './session-reports-filter-input.component.html',
  styleUrl: './session-reports-filter-input.component.scss',
})
export class SessionReportsFilterInputComponent implements ISubbmittable {
  @Output() emitFilter: EventEmitter<void> = new EventEmitter();
  protected startDate: SessionReportPeriodContract;
  protected endDate: SessionReportPeriodContract;

  protected startDateStringRepresentation: string;
  protected endDateStringRepresentation: string;

  public constructor(
    private readonly _dataService: SessionReportsDataService,
    private readonly _paginationService: SessionReportsPaginationService,
    private readonly _authService: AuthService,
  ) {
    this.startDate = {} as SessionReportPeriodContract;
    this.endDate = {} as SessionReportPeriodContract;
  }

  public submit(): void {
    this.tryParseDates(this.startDate, this.startDateStringRepresentation);
    this.tryParseDates(this.endDate, this.endDateStringRepresentation);
    const policy: IFetchPolicy<ControlWeekReportInterface[]> =
      new SessionReportPeriodFetchPolicy(
        this._authService,
        this.startDate,
        this.endDate,
      );
    policy.addPages(
      this._paginationService.currentPage,
      this._paginationService.pageSize,
    );
    this._dataService.setPolicy(policy);
    this.emitFilter.emit();
  }

  protected tryParseDates(
    contract: SessionReportPeriodContract,
    stringDate: string,
  ): void {
    if (!stringDate) return;
    try {
      const parts: string[] = stringDate.split('-');
      const year: number = Number(parts[0]);
      const month: number = Number(parts[1]);
      const day: number = Number(parts[2]);
      contract.day = day;
      contract.month = month;
      contract.year = year;
    } catch {
      contract.day = null;
      contract.month = null;
      contract.year = null;
    }
  }
}
