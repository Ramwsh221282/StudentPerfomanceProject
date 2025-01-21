import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { UserRecord } from '../../services/user-table-element-interface';
import { User } from '../../../../../../pages/user-page/services/user-interface';
//import { BASE_API_URI } from '../../../../../../shared/models/api/api-constants';
import { AppConfigService } from '../../../../../../app.config.service';
import { AuthService } from '../../../../../../pages/user-page/services/auth.service';

export class UsersFilterFetchPolicy implements IFetchPolicy<UserRecord[]> {
  private readonly _user: User;
  private readonly _apiUri: string;
  private readonly _httpHeaders: HttpHeaders;
  private _httpParams: HttpParams;

  public constructor(
    user: User,
    private readonly _authService: AuthService,
    private readonly _appConfig: AppConfigService,
  ) {
    this._user = { ...user };
    //this._apiUri = `${BASE_API_URI}/api/users/filter`;
    this._httpHeaders = this.buildHttpHeaders();
    this._apiUri = `${this._appConfig.baseApiUri}/api/users/filter`;
  }

  public executeFetchPolicy(httpClient: HttpClient): Observable<UserRecord[]> {
    const headers = this._httpHeaders;
    const params = this._httpParams;
    return httpClient.get<UserRecord[]>(this._apiUri, {
      headers: headers,
      params,
    });
  }

  public addPages(page: number, pageSize: number): void {
    this._httpParams = this.buildHttpParams(page, pageSize);
  }

  private buildHttpParams(page: number, pageSize: number) {
    return new HttpParams()
      .set('page', page)
      .set('pageSize', pageSize)
      .set('name', this._user.name)
      .set('surname', this._user.surname)
      .set('patronymic', this._user.patronymic)
      .set('email', this._user.email)
      .set('role', this._user.role);
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._authService.userData.token);
  }
}
