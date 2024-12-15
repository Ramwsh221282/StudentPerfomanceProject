import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { EducationDirection } from '../education-direction-interface';
//import { BASE_API_URI } from '../../../../../../shared/models/api/api-constants';
import { AuthService } from '../../../../../users/services/auth.service';
import { AppConfigService } from '../../../../../../app.config.service';

export class FilterFetchPolicy implements IFetchPolicy<EducationDirection[]> {
  private readonly _baseApiUri: string;
  private readonly _direction: EducationDirection;
  private _headers: HttpHeaders;
  private _params: HttpParams;

  public constructor(
    direction: EducationDirection,
    private readonly authService: AuthService,
    private readonly _appConfig: AppConfigService,
  ) {
    //this._baseApiUri = `${BASE_API_URI}/api/education-direction/filter`;
    this._baseApiUri = `${this._appConfig.baseApiUri}/api/education-direction/filter`;
    this._direction = direction;
    this.buildHeaders();
    this.buildParams();
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
    this.appendPages(page, pageSize);
  }

  private appendPages(page: number, pageSize: number): void {
    this._params = new HttpParams()
      .set('page', page)
      .set('pageSize', pageSize)
      .set('filterCode', this._direction.code)
      .set('filterName', this._direction.name)
      .set('filterType', this._direction.type);
  }

  private buildParams(): void {
    this._params = new HttpParams()
      .set('filterCode', this._direction.code)
      .set('filterName', this._direction.name)
      .set('filterType', this._direction.type);
  }

  private buildHeaders(): void {
    this._headers = new HttpHeaders().set(
      'token',
      this.authService.userData.token,
    );
  }
}
