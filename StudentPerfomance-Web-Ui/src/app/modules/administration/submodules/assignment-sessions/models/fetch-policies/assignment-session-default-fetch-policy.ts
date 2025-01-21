import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { AssignmentSession } from '../assignment-session-interface';
import { AuthService } from '../../../../../../pages/user-page/services/auth.service';
import { AppConfigService } from '../../../../../../app.config.service';

//import { BASE_API_URI } from '../../../../../../shared/models/api/api-constants';

export class AssignmentSessionDefaultFetchPolicy
  implements IFetchPolicy<AssignmentSession[]>
{
  private readonly _apiUri: string;
  private readonly _httpHeaders: HttpHeaders;
  private _httpParams: HttpParams;

  public constructor(
    private readonly _authService: AuthService,
    private readonly _appConfig: AppConfigService,
  ) {
    //this._apiUri = `${BASE_API_URI}/api/assignment-sessions/byPage`;
    this._apiUri = `${this._appConfig.baseApiUri}/api/assignment-sessions/byPage`;
    this._httpHeaders = this.buildHttpHeaders();
  }

  public executeFetchPolicy(
    httpClient: HttpClient,
  ): Observable<AssignmentSession[]> {
    const headers = this._httpHeaders;
    const params = this._httpParams;
    return httpClient.get<AssignmentSession[]>(this._apiUri, {
      headers: headers,
      params,
    });
  }

  public addPages(page: number, pageSize: number): void {
    this._httpParams = this.buildHttpParams(page, pageSize);
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._authService.userData.token);
  }

  private buildHttpParams(page: number, pageSize: number): HttpParams {
    return new HttpParams().set('page', page).set('pageSize', pageSize);
  }
}
