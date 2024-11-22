import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../models/fetch-policices/fetch-policy-interface';
import { ControlWeekReportInterface } from '../../../Models/Data/control-week-report-interface';
import { SessionReportPeriodContract } from './session-report-period-filter-contract';
import { AuthService } from '../../../../../../modules/users/services/auth.service';
import { BASE_API_URI } from '../../../../../models/api/api-constants';
import { TokenPayloadBuilder } from '../../../../../models/common/token-contract/token-payload-builder';
import { PaginationPayloadBuilder } from '../../../../../models/common/pagination-contract/pagination-payload-builder';

export class SessionReportPeriodFetchPolicy
  implements IFetchPolicy<ControlWeekReportInterface[]>
{
  private readonly _startDate: SessionReportPeriodContract;
  private readonly _endDate: SessionReportPeriodContract;
  private readonly _apiUri: string;
  private _payload: object;

  public constructor(
    private readonly _authService: AuthService,
    startDate: SessionReportPeriodContract | null,
    endDate: SessionReportPeriodContract | null,
  ) {
    if (startDate) this._startDate = startDate;
    if (endDate) this._endDate = endDate;
    this._apiUri = `${BASE_API_URI}/app/assignment-sessions/paged-filtered`;
  }

  public executeFetchPolicy(
    httpClient: HttpClient,
  ): Observable<ControlWeekReportInterface[]> {
    const payload = this._payload;
    return httpClient.post<ControlWeekReportInterface[]>(this._apiUri, payload);
  }

  public addPages(page: number, pageSize: number): void {
    this._payload = {
      token: TokenPayloadBuilder(this._authService.userData),
      pagination: PaginationPayloadBuilder(page, pageSize),
      period: {
        startPeriod: this._startDate,
        endPeriod: this._endDate,
      },
    };
  }
}
