import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { EducationPlan } from '../education-plan-interface';
//import { BASE_API_URI } from '../../../../../../shared/models/api/api-constants';
import { AuthService } from '../../../../../../pages/user-page/services/auth.service';
import { AppConfigService } from '../../../../../../app.config.service';

export class DefaultFetchPolicy implements IFetchPolicy<EducationPlan[]> {
  private readonly _baseApiUri;
  private readonly _authService: AuthService;
  private _params: HttpParams;
  private _headers: HttpHeaders;

  public constructor(
    authService: AuthService,
    private readonly _appConfig: AppConfigService,
  ) {
    //this._baseApiUri = `${BASE_API_URI}/api/education-plans/`;
    this._baseApiUri = `${this._appConfig.baseApiUri}/api/education-plans/`;
    this._authService = authService;
    this.buildHttpParams();
    this.buildHttpHeaders();
  }

  public executeFetchPolicy(
    httpClient: HttpClient,
  ): Observable<EducationPlan[]> {
    const params = this._params;
    const headers = this._headers;
    return httpClient.get<EducationPlan[]>(this._baseApiUri, {
      headers: headers,
      params,
    });
  }

  public addPages(page: number, pageSize: number): void {
    this.appendPages(page, pageSize);
  }

  private appendPages(page: number, pageSize: number): void {
    this._params = new HttpParams().set('page', page).set('pageSize', pageSize);
  }

  private buildHttpParams(): void {
    this._params = new HttpParams();
  }

  private buildHttpHeaders(): void {
    this._headers = new HttpHeaders().set(
      'token',
      this._authService.userData.token,
    );
  }
}
