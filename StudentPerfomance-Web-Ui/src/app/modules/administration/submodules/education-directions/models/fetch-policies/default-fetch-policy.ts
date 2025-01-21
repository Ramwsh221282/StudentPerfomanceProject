import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { EducationDirection } from '../education-direction-interface';
//import { BASE_API_URI } from '../../../../../../shared/models/api/api-constants';
import { User } from '../../../../../../pages/user-page/services/user-interface';
import { AuthService } from '../../../../../../pages/user-page/services/auth.service';
import { AppConfigService } from '../../../../../../app.config.service';

export class DefaultFetchPolicy implements IFetchPolicy<EducationDirection[]> {
  private readonly _baseApiUri: string;
  private readonly _user: User;
  private _headers: HttpHeaders;
  private _params: HttpParams;

  public constructor(
    private readonly authService: AuthService,
    private readonly _appConfig: AppConfigService,
  ) {
    //this._baseApiUri = `${BASE_API_URI}/api/education-direction/`;
    this._baseApiUri = `${this._appConfig.baseApiUri}/api/education-direction/`;
    this._user = { ...authService.userData };
    this.buildHttpHeaders();
  }

  public executeFetchPolicy(
    httpClient: HttpClient,
  ): Observable<EducationDirection[]> {
    const headers = this._headers;
    const params = this._params;
    return httpClient.get<EducationDirection[]>(this._baseApiUri, {
      headers: headers,
      params,
    });
  }

  public addPages(page: number, pageSize: number): void {
    this.buildHttpParams(page, pageSize);
  }

  private buildHttpParams(page: number, pageSize: number): void {
    this._params = new HttpParams().set('page', page).set('pageSize', pageSize);
  }

  private buildHttpHeaders(): void {
    this._headers = new HttpHeaders().set('token', this._user.token);
  }
}
