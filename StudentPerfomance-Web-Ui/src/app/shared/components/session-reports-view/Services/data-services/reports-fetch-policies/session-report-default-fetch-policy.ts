import { IFetchPolicy } from '../../../../../models/fetch-policices/fetch-policy-interface';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../../../../../../pages/user-page/services/auth.service';
//import { BASE_API_URI } from '../../../../../models/api/api-constants';
import { ControlWeekReportInterface } from '../../../Models/Data/control-week-report-interface';
import { AppConfigService } from '../../../../../../app.config.service';

export class SessionReportDefaultFetchPolicy
  implements IFetchPolicy<ControlWeekReportInterface[]>
{
  private readonly _baseApiUri: string;
  private readonly _httpHeaders: HttpHeaders;
  private _httpParams: HttpParams;

  public constructor(
    private readonly _authService: AuthService,
    private readonly _appConfig: AppConfigService,
  ) {
    //this._baseApiUri = `${BASE_API_URI}/app/assignment-sessions/reports-paged`;
    this._baseApiUri = `${this._appConfig.baseApiUri}/app/assignment-sessions/reports-paged`;
    this._httpHeaders = this.buildHttpHeaders();
  }

  public executeFetchPolicy(
    httpClient: HttpClient,
  ): Observable<ControlWeekReportInterface[]> {
    const headers = this._httpHeaders;
    const params = this._httpParams;
    return httpClient.get<ControlWeekReportInterface[]>(this._baseApiUri, {
      headers: headers,
      params: params,
    });
  }

  public addPages(page: number, pageSize: number): void {
    this._httpParams = this.buildHttpParams(page, pageSize);
  }

  private buildHttpParams(page: number, pageSize: number): HttpParams {
    return new HttpParams().set('page', page).set('pageSize', pageSize);
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._authService.userData.token);
  }
}
