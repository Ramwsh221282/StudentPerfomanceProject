import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../models/fetch-policices/fetch-policy-interface';
import { ControlWeekReportInterface } from '../../../Models/Data/control-week-report-interface';
import { AuthService } from '../../../../../../pages/user-page/services/auth.service';
//import { BASE_API_URI } from '../../../../../models/api/api-constants';
import { TokenPayloadBuilder } from '../../../../../models/common/token-contract/token-payload-builder';
import { PaginationPayloadBuilder } from '../../../../../models/common/pagination-contract/pagination-payload-builder';
import { AppConfigService } from '../../../../../../app.config.service';

export class SessionReportPeriodFetchPolicy
  implements IFetchPolicy<ControlWeekReportInterface[]>
{
  private readonly _year: number;
  private readonly _sessionNumber: number;
  private readonly _sessionSeason: string;
  private readonly _apiUri: string;
  private _payload: object;

  public constructor(
    private readonly _authService: AuthService,
    year: number,
    sessionNumber: number,
    sessionSeason: string,
    private readonly _configService: AppConfigService,
  ) {
    //this._apiUri = `${BASE_API_URI}/app/assignment-sessions/paged-filtered`;
    this._year = year;
    this._sessionNumber = sessionNumber;
    this._sessionSeason = sessionSeason;
    this._apiUri = `${this._configService.baseApiUri}/app/assignment-sessions/paged-filtered`;
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
      year: this._year,
      number: this._sessionNumber,
      season: this._sessionSeason,
    };
  }
}
