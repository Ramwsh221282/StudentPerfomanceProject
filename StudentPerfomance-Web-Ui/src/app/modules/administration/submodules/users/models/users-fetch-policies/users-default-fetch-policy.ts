import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IFetchPolicy } from '../../../../../../shared/models/fetch-policices/fetch-policy-interface';
import { UserRecord } from '../../services/user-table-element-interface';
import { User } from '../../../../../users/services/user-interface';
import { AppConfigService } from '../../../../../../app.config.service';

//import { BASE_API_URI } from '../../../../../../shared/models/api/api-constants';

export class DefaultFetchPolicy implements IFetchPolicy<UserRecord[]> {
  private readonly _user: User;
  private readonly _apiUri: string;
  private readonly _httpHeaders: HttpHeaders;
  private _httpParams: HttpParams;

  public constructor(
    user: User,
    private readonly _appCofig: AppConfigService,
  ) {
    this._user = user;
    //this._apiUri = `${BASE_API_URI}/api/users`;
    this._apiUri = `${this._appCofig.baseApiUri}/api/users`;
    this._httpHeaders = this.buildHttpHeaders();
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
    return new HttpParams().set('page', page).set('pageSize', pageSize);
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._user.token);
  }
}
